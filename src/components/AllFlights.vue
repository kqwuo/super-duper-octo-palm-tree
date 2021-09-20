

<template>
  <div class="container">

    <h1>{{ msg }}</h1>
    <br>

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
                <td><button type="button" @click="Book(flight.idRoute)" class="btn btn-info">Book</button></td>
                <td>{{ flight.availableSeats }}</td>
            </tr>
        </tbody>
    </table>

</div> 
</template>

<script lang="ts">
import { Vue } from "vue-class-component";
import axios from "axios"

export default class AllFlights extends Vue {
  public allFlights : any[] = [];

  async mounted () {
    this.allFlights = (await axios.get<any[]>('http://localhost:5000/api/route/getAllRoutes')).data;
    console.log(this.allFlights)
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
}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
