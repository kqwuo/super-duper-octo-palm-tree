<template>
  <div class="container">
    <br /><br />

    <select @change="onChangeCurrency($event)" v-model="currentCurrency">
      <option
        v-for="item in allCurrencies"
        :value="getValueCurrencies(item.type)"
        :key="item.type"
      >
        {{ getValueCurrencies(item.type) }}
      </option>
    </select>

    <br><br>

    <input type="date" v-model="this.date" v-on:change="getFlights()">

    <!-- <button @click="getFlights" -->

    <br /><br /><br />

    <table class="table table-striped table-bordered">
        <thead>
            <tr>
                <th>Name</th>
                <th>Destination</th>
                <th>Price</th>
                <th>Reservation</th>
                <th>Tickets number</th>
                <th>Reserver par</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="flight in allFlights" :key="flight.idFlight">
                <td>{{ flight.arrivalPlace }}</td>
                <td>{{ flight.departurePlace }}</td>
                <td>{{ (flight.basePrice * currencyRate).toFixed(2) }} &nbsp; {{ symbolCurrency }}</td>
                <td><button type="button" @click="showModal(flight)" class="btn btn-info">Book</button></td> <!-- Book(flight.idRoute) -->
                <td>{{ flight.availableSeats }}</td>
                <td>{{flight.flightSource}}</td>
            </tr>
        </tbody>
    </table>

    <BookModal
      v-if="isModalVisible"
      v-show="isModalVisible"
      :flight="selectedFlight"
      :currency="currency"
      :date="date"
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
import { Currency, CurrencySymbol, CurrencyType } from "../models/currency";

@Options({
  components: {
    BookModal,
  },
})
export default class AllFlights extends Vue {
  public allFlights : Array<Flight> = new Array<Flight>();
  public allCurrencies: Currency[] = [];
  public currentCurrency: string = "EUR";
  public symbolCurrency: string = "€";
  public isModalVisible: boolean = false;
  public currencyRate: number = 1;
  public currency?: Currency;
  public idFlight : string = "";
  public selectedFlight: Flight | null = null;
  public date: string = new Date().toISOString().split("T")[0];

  async mounted () {
    await this.getFlights();
    const currencyType : string[] = this.ToArray(CurrencyType);

    this.allCurrencies = currencyType.map<Currency>((x : any, index) => {
      return { symbol: CurrencySymbol[x as keyof typeof CurrencySymbol], type: index };
    });
    this.currency = this.allCurrencies[0];

    this.currentCurrency = CurrencyType[this.allCurrencies[0].type];
    this.symbolCurrency = this.allCurrencies[0].symbol;

    this.currencyRate = (
      await axios.get<number>(
        `http://10.10.10.163:5000/api/currency/${this.allCurrencies[0].type}`
      )
    ).data;

    this.currency.rate = this.currencyRate;

  };

  async getFlights(): Promise<void> {
    this.allFlights = (await axios.get<Array<Flight>>('http://localhost:5000/api/flight/getAllFlights', {
      params: {
        date: this.date
      }
    })).data;
  }

  async onChangeCurrency(event: any) {

    const newCurrency = this.allCurrencies.find(x => x.type === parseInt(CurrencyType[event.target.value]))
    if(newCurrency){
    this.currencyRate = (
      await axios.get<number>(
        `http://10.10.10.163:5000/api/currency/${newCurrency.type}`
      )
    ).data;

    this.currency = newCurrency;
    this.currency.rate = this.currencyRate;
    this.currentCurrency = CurrencyType[newCurrency.type];
    this.symbolCurrency = newCurrency.symbol;
    }
  }

  getValueCurrencies(type: CurrencyType) {
    return CurrencyType[type];
  }

  ToArray(enumme: any) {
    return Object.keys(enumme)
      .filter((value: string) => !isNaN(Number(value)))
      .map((key) => enumme[key]);
  }

  async Book(idFlight: string, order: Order) {
    console.log(order);
    var isSuccess = await axios.post<boolean>('http://10.10.10.163:5000/api/order/' + idFlight, order)

    if (isSuccess){
      const bookedFlightResponse : Flight = (await axios.get<Flight>('http://10.10.10.163:5000/api/flight/getFlight/'+idFlight)).data
      const idxFlight = this.allFlights.findIndex(flight => flight.idFlight === bookedFlightResponse.idFlight)
      this.allFlights.splice(idxFlight, 1, bookedFlightResponse)
    }
  }

  showModal(flight: Flight) {
    this.isModalVisible = true;
    this.selectedFlight = flight;
  }
  closeModal() {
    this.isModalVisible = false;
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
