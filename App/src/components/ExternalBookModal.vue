<template>
  <div class="modal-mask">
    <div class="modal-wrapper">
      <div class="modal-container">
        <div class="modal-body">
          <span style="color: red; font-weight: bold">{{ errorMessage }}</span>
          <div class="summary">
            Nom de l'acheteur :
            <input v-model="this.order.user.name" type="text" /> Prix total :
            {{ recalculate() * (this.currency?.rate ?? 1) }}&nbsp;{{
              this.currency?.symbol
            }}
          </div>
          <table class="ticket-list">
            <tr>
              <th>Nom</th>
              <th>Prénom</th>
              <th>Type</th>
              <th>Nationalité</th>
              <th>Prix</th>
              <th>Options</th>
              <th></th>
            </tr>
            <tr
              v-for="(ticket, idx) in this.order.ticketList"
              :value="ticket"
              :key="idx"
            >
              <td>
                <input v-model="ticket.lastName" />
              </td>
              <td>
                <input v-model="ticket.firstName" />
              </td>
              <td>
                <select v-model="ticket.userType">
                  <option :value="this.userTypeEnum.adult">
                    <p>Adulte</p>
                  </option>
                  <option :value="this.userTypeEnum.child">
                    <p>Enfant</p>
                  </option>
                </select>
              </td>
              <td>
                <select v-model="ticket.nationality">
                  <option>
                    <!-- :value="this.nationalityType.french" -->
                    <p>Française</p>
                  </option>
                  <option>
                    <!-- :value="this.nationalityType.other" -->
                    <p>Autre</p>
                  </option>
                </select>
              </td>
              <td>
                <input
                  type="number"
                  v-model="ticket.nbAdditionalLuggage"
                  @click="recalculate()"
                  min="0"
                  max="3"
                />
              </td>
              <span v-if="this.flight.flightOptions">
                <div
                  v-for="item in this.flight.flightOptions"
                  :key="item.option"
                >
                  <td>
                    {{ item.option }}
                    <input type="checkbox" @click="" true-value="yes" false-value="no" />
                  </td>
                </div>
              </span>
              <td>
                {{
                  (
                    (this.flight?.basePrice +
                      this.flight?.additionalLuggagePrice *
                        ticket.nbAdditionalLuggage) *
                    (this.currency?.rate ?? 1)
                  ).toFixed(2)
                }}
                {{ this.currency?.symbol }}
              </td>
              <td>
                <button
                  @click="this.order.ticketList.splice(idx, 1)"
                  v-if="this.order.ticketList.length > 1"
                >
                  -
                </button>
              </td>
            </tr>
          </table>
          <button @click="addTicket()">+</button>
        </div>

        <div class="modal-footer">
          <button
            type="button"
            @click="this.$emit('close')"
            class="btn btn-secondary"
          >
            Fermer
          </button>
          <button type="button" @click="validate()" class="btn btn-info">
            Valider
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Flight } from "../models/flight";
import axios from "axios";
import { Vue } from "vue-class-component";
import { Prop } from "vue-property-decorator";
import { Currency, CurrencyType } from "../models/currency";
import { Order } from "../models/order";
import { ExternalTicket, Ticket } from "../models/ticket";
import { UserType } from "../models/usertype";

export default class ExternalBookModal extends Vue {
  @Prop() public flight?: Flight;
  @Prop() public currency?: Currency;
  order: Order = this.createDefault();

  public errorMessage: string = "";

  userTypeEnum = UserType;

  createTicket(): ExternalTicket {
    const ticket: ExternalTicket = {
      flight:{},
      customerName: "",
      customerNationality: "",
      date: "",
      payedPrice: 0,
      options: [],
    };

    return ticket;
  }

  createDefault(): Order {
    return {
      user: {
        name: "",
      },
      nbBought: 0,
      ticketList: new Array<ExternalTicket>(this.createTicket()),
      totalBasePrice: 0,
      totalAdditionalPrice: 0,
      totalDiscountedBasePrice: 0,
      totalPrice: 0,
      usedCurrency: CurrencyType.EUR,
      exchangeRate: this.currency?.rate!,
      isPaid: false,
    };
  }

  async Book(idFlight: string, order: Order) {
    console.log(order);
    var isSuccess = await axios.post<boolean>(
      "http://10.10.10.163:5000/api/order/" + idFlight,
      order
    );

    if (isSuccess) {
      const parent: any = this.$parent;
      parent.reloadFlight(idFlight);
    }
  }

  addTicket() {
    this.order.ticketList.push(this.createTicket());
    this.recalculate();
  }

  recalculate() {
    let totalPrice = 0;
    this.order.ticketList.forEach(
      (x) =>
        (totalPrice +=
          this.flight!.basePrice +
          this.flight!.additionalLuggagePrice * x.nbAdditionalLuggage)
    );

    return totalPrice;
  }

  close() {
    this.$emit("close");
  }

  validate() {
    this.errorMessage = "";
    const ticket = this.order.ticketList.find(
      (x) => x.lastName === "" || x.firstName == ""
    );

    if (this.order.user.name && !ticket) {
      this.Book(this.flight!.idFlight, this.order);
      this.order = this.createDefault();
      this.$emit("close");
    } else {
      this.errorMessage =
        "Un des champs requis : Nom, Prenom est vide.Veuillez remplir le necessaire";
    }
  }
}
</script>

<style>
.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: opacity 0.3s ease;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}

.modal-container {
  width: 80%;
  margin: 0px auto;
  padding: 20px 30px;
  background-color: #fff;
  border-radius: 2px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.33);
  transition: all 0.3s ease;
  font-family: Helvetica, Arial, sans-serif;
}

.modal-body {
  color: grey;
}

.ticket-list {
  width: 100%;
  margin-bottom: 15px;
}
.input-item {
  margin: 5px;
}
</style>