<template>
  <div class="container">

    <h1>{{ msg }}</h1>
    <br>

    <h3>{{ info }}</h3>

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
            <tr v-for="flight in flights" :key="flight.id">
                <td>{{flight.name}}</td>
                <td>{{flight.destination}}</td>
                <td>{{flight.departure}}</td>
                <td>{{flight.arrival}}</td>
                <td>{{flight.price}}</td>
                <td><button type="button" @click="Book(flight.name)" class="btn btn-info">Book</button></td>
                <td>{{flight.tickets}}</td>
            </tr>
        </tbody>
    </table>

</div> 
</template>

<script>

import { ref } from 'vue';
const axios = require('axios');

export default {
  name: "Index",
  props: {
    msg: String,
  },
  data () {
    return {
      info: null
    }
  },
  mounted () {
    axios
      .get('localhost:5000/api/route/getAllRoutes')
      .then(response => (this.info = response.data))
  },
  setup() {
    const flights = ref([
      { name: 'DTW', destination: 'JFK', departure: '20:00', arrival: '22:30', price: '300€', tickets: 300 },
      { name: 'CDG', destination: 'DTW', departure: '18:25', arrival: '23:30', price: '700€', tickets: 700 },
      { name: 'JFR', destination: 'CDG', departure: '14:30', arrival: '19:45', price: '1000€', tickets: 1000 },
    ]);

    return {
      flights
    };
  },
  methods: {
    Book(name) {
      // faire un post avec le nom de l'avion voulu
      console.log(name);
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
