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
                <th>Reservation</th>
                <th>Tickets number</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="flight in allFlights" :key="flight.idRoute">
                <td>{{ flight.arrivalPlace }}</td>
                <td>{{ flight.departurePlace }}</td>
                <td>?</td>
                <td>?</td>
                <td>{{ flight.price }}</td>
                <td><button type="button" @click="showModal(flight.idRoute)" class="btn btn-info">Book</button></td> <!-- Book(flight.idRoute) -->
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

@Options({
  components: {
    BookModal,
  },
})

export default class AllFlights extends Vue {
  public allFlights : any[] = [];
  public allCurrencies : any[] = [];
  public isModalVisible : boolean = false;

  public idFlight : string = "";

  async mounted () {
    this.allFlights = (await axios.get<any[]>('http://localhost:5000/api/route/getAllRoutes')).data;
  };
  async Book(name : string) {
    var isSuccess = await axios.post<boolean>('http://localhost:5000/api/order/order/'+name, {
      user: {
        name: 'JL'
      },
      nbBought: 3
    })

    if (isSuccess){
      const bookedFlightResponse : any = (await axios.get<any>('http://localhost:5000/api/route/getBookedRoute/'+name)).data
      const idxFlight = this.allFlights.findIndex(flight => flight.idRoute === bookedFlightResponse.idRoute)
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
