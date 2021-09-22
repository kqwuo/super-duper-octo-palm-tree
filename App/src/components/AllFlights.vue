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

    <br /><br /><br />

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
                <td>{{ (flight.basePrice * currencyRate).toFixed(2) }} &nbsp; {{ symbolCurrency }}</td>
                <td>{{ flight.additionalLuggagePrice }}</td>
                <td><button type="button" @click="showModal(flight)" class="btn btn-info">Book</button></td> <!-- Book(flight.idRoute) -->
                <td>{{ flight.availableSeats }}</td>
            </tr>
        </tbody>
    </table>

    <!--<BookModal
      v-if="isModalVisible"
      v-show="isModalVisible"
      :flight="selectedFlight"
      :currency="currency"
      @close="closeModal"
    />-->

    <ExternalBookModal
      v-if="isModalVisible"
      v-show="isModalVisible"
      :flight="selectedFlight"
      :currency="currency"
      @close="closeModal"
    />
  </div>
</template>

<script lang="ts">
import { Vue, Options } from 'vue-class-component';
import axios from "axios"
import BookModal from './BookModal.vue';
import ExternalBookModal from './ExternalBookModal.vue';
import { Flight } from "../models/flight";
import { Currency, CurrencySymbol, CurrencyType } from "../models/currency";

@Options({
  components: {
    BookModal,
    ExternalBookModal
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

  async mounted () {
    this.allFlights = (await axios.get<Array<Flight>>('http://localhost:5000/api/flight/getAllFlights')).data;
    const currencyType : string[] = this.ToArray(CurrencyType);

    this.allCurrencies = currencyType.map<Currency>((x : any, index) => {
      return { symbol: CurrencySymbol[x as keyof typeof CurrencySymbol], type: index };
    });
    this.currency = this.allCurrencies[0];

    this.currentCurrency = CurrencyType[this.allCurrencies[0].type];
    this.symbolCurrency = this.allCurrencies[0].symbol;

    this.currencyRate = (
      await axios.get<number>(
        `http://localhost:5000/api/currency/${this.allCurrencies[0].type}`
      )
    ).data;

    this.currency.rate = this.currencyRate;

  };

  async onChangeCurrency(event: any) {

    const newCurrency = this.allCurrencies.find(x => x.type === parseInt(CurrencyType[event.target.value]))
    if(newCurrency){
    this.currencyRate = (
      await axios.get<number>(
        `http://localhost:5000/api/currency/${newCurrency.type}`
      )
    ).data;

    this.currency = newCurrency;
    this.currency.rate = this.currencyRate;
    this.currentCurrency = CurrencyType[newCurrency.type];
    this.symbolCurrency = newCurrency.symbol;
    }
  }

  async reloadFlight(idFlight: string) {
    const bookedFlightResponse : Flight = (await axios.get<Flight>('http://localhost:5000/api/flight/getFlight/'+idFlight)).data
    const idxFlight = this.allFlights.findIndex(flight => flight.idFlight === bookedFlightResponse.idFlight)
    this.allFlights.splice(idxFlight, 1, bookedFlightResponse)
  }

  getValueCurrencies(type: CurrencyType) {
    return CurrencyType[type];
  }

  ToArray(enumme: any) {
    return Object.keys(enumme)
      .filter((value: string) => !isNaN(Number(value)))
      .map((key) => enumme[key]);
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
