<template>
  <div class="container">

    <h1>{{ msg }}</h1>
    <br>

    <!--<h3>{{ info }}</h3>-->

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
//const axios = require('axios');

export default {
  name: "Index",
  props: {
    msg: String,
  },
  data () {
    return {
      //info: null
    }
  },
  mounted () {
    /*axios
      .get('localhost:5000/api/flights')
      .then(response => (this.info = response))*/
  },
  setup() {
    const flights = ref([
      { name: 'GOTA', destination: 'Aéroport Charles de Gaulle', departure: '20:00', arrival: '22:30', price: '100€', tickets: 100 },
      { name: 'NORA', destination: 'Aéroport de Londres', departure: '18:25', arrival: '23:30', price: '150€', tickets: 100 },
      { name: 'MONA', destination: 'Aéroport de Helsinki', departure: '14:30', arrival: '19:45', price: '75€', tickets: 100 },
      { name: 'TUPE', destination: 'Aéroport de Hong Hong', departure: '22:35', arrival: '02:43', price: '22€', tickets: 100 },
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
