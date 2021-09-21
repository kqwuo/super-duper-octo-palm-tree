<template>
  <div class="container">

    <br><br>

    <select v-model="type">
       <option v-for="item in inventory" :value="item" :key="item.id">
        {{ item.name }}
      </option>
    </select>

    <br><br><br>

    <table class="table table-striped table-bordered">
        <thead>
            <tr>
                <th>Name</th>
                <th>Destination</th>
                <th>Departure</th>
                <th>Arrival</th>
                <th>Price</th>
                <th>Additional luggage price</th>
                <th>Reservation</th>
                <th>Tickets number</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="flight in allFlights" :key="flight.idFlight">
                <td>{{ flight.arrivalPlace }}</td>
                <td>{{ flight.departurePlace }}</td>
                <td>?</td>
                <td>?</td>
                <td>{{ flight.basePrice }}</td>
                <td>{{ flight.additionalLuggagePrice }}</td>
                <td><button type="button" @click="showModal(flight.idFlight)" class="btn btn-info">Book</button></td> <!-- Book(flight.idRoute) -->
                <td>{{ flight.availableSeats }}</td>
            </tr>
        </tbody>
    </table>

    <BookModal
      v-show="isModalVisible"
      :idFlight="idFlight"
      @close="closeModal"
    />

</div> 
</template>

<script lang="ts">
import { Vue, Options } from 'vue-class-component';
import axios from "axios"
import BookModal from './BookModal.vue';
import { Flight } from "../models/flight";
import { Order } from '../models/order';

@Options({
  components: {
    BookModal,
  },
})

export default class AllFlights extends Vue {
  public allFlights : Array<Flight> = new Array<Flight>();
  public allCurrencies : any[] = [];
  public isModalVisible : boolean = false;

  public idFlight : string = "";

  async mounted () {
    this.allFlights = (await axios.get<Array<Flight>>('http://localhost:5000/api/flight/getAllFlights')).data;
  };
  async Book(idFlight: string, order: Order) {
    console.log(order);
    var isSuccess = await axios.post<boolean>('http://localhost:5000/api/order/' + idFlight, order)

    if (isSuccess){
      const bookedFlightResponse : Flight = (await axios.get<Flight>('http://localhost:5000/api/flight/getFlight/'+idFlight)).data
      const idxFlight = this.allFlights.findIndex(flight => flight.idFlight === bookedFlightResponse.idFlight)
      this.allFlights.splice(idxFlight, 1, bookedFlightResponse)
    }
  }

  showModal(id: string) {
    this.isModalVisible = true;
    this.idFlight = id;
  }
  closeModal() {
    this.isModalVisible = false;
  }
}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
