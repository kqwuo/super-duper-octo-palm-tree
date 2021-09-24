<template>
  <div class="modal-mask">
    <div class="modal-wrapper">
      <div class="modal-container">
        <div class="modal-body">
          <span style="color:red; font-weight:bold">{{errorMessage}}</span>
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
              <template v-if="flight.additionalFields">
                <th v-for="field in flight.additionalFields" :key="field.fieldName">{{ field.label ?? field.fieldName }}</th>
              </template>
              <th>Options</th>
              <th>Prix</th>
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
              <template v-if="flight.additionalFields">
                <td v-for="field in ticket.additionalFields" :key="field.fieldName">
                  <input v-model="field.value">
                </td>
              </template>
              <td>
                <div v-for="option in ticket.options" :key="option.label">
                  <i>{{option.Label ?? option.fieldName}}</i>
                  <input @click="this.recalculate()" v-if="option.returnType == 'number'" type="number" min="0" v-model="option.value">
                  <input @click="this.recalculate()" v-else-if="option.returnType == 'bool'" type="checkbox" v-model="option.value">
                </div>
              </td>
              <td>
                {{
                  recalculateTicket(ticket)
                   
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
          <button
            type="button"
            @click="
             validate()
            "
            class="btn btn-info"
          >
            Valider
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Flight } from "@/models/flight";
import { FlightSource } from "@/models/flightSource.enum";
import { Vue } from "vue-class-component";
import { Prop } from "vue-property-decorator";
import { Currency, CurrencyType } from "../models/currency";
import { Order } from "../models/order";
import { Ticket } from "../models/ticket";
import { UserType } from "../models/usertype";

export default class BookModal extends Vue {
  @Prop() public flight?: Flight;
  @Prop() public currency?: Currency;
  order: Order = this.createDefault();

  public errorMessage: string = "";

  userTypeEnum = UserType;

  createTicket(): Ticket {
    const ticket: Ticket = {
      firstName: "",
      lastName: "",
      userType: UserType.adult,
      basePrice: 0,
      paidTotal: 0,
      basePriceDiscount: 0,
      discountedBasePrice: 0,
      options: [],
      additionalFields: []
    };
    if (this.flight) {
      this.flight.options.forEach( o => {
        ticket.options.push({ label: o.label, fieldName: o.fieldName, price: o.price, value: o.value, returnType: o.returnType});
      });
      if (this.flight.additionalFields)
        this.flight.additionalFields.forEach( af => {
          ticket.additionalFields.push({ label: af.label, fieldName: af.fieldName, value: af.value, returnType: af.returnType});
        });
      ticket.basePrice = this.flight.basePrice;
    }

    return ticket;
  }

  createDefault(): Order {
    return {
      user: {
        name: "",
      },
      nbBought: 0,
      ticketList: new Array<Ticket>(this.createTicket()),
      date: "00-00-0000",
      totalBasePrice: 0,
      totalAdditionalPrice: 0,
      totalDiscountedBasePrice: 0,
      totalPrice: 0,
      usedCurrency: CurrencyType.EUR,
      exchangeRate: this.currency?.rate!,
      isPaid: false,
      flightSource: FlightSource.internal
    };
  }

  addTicket() {
    this.order.ticketList.push(this.createTicket());
    this.recalculate();
  }

  recalculateTicket(ticket : Ticket){
     let totalPrice = ticket.basePrice;
      ticket.options.forEach( o => {
          if (o.returnType == 'number')
            totalPrice += o.value * o.price;
          else if (o.returnType == 'bool' && o.value === true)
            totalPrice += o.price;
        });

         totalPrice*=(this.currency?.rate ?? 1);
        return(totalPrice.toFixed(2))
  }

  recalculate() {
    let totalPrice = 0;
    this.order.ticketList.forEach(
      (x) => {
        totalPrice += this.flight!.basePrice;
        x.options.forEach( o => {
          if (o.returnType == 'number')
            totalPrice += o.value * o.price;
          else if (o.returnType == 'bool' && o.value === true)
            totalPrice += o.price;
        });
      }
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
      const parent: any = this.$parent;
      this.order.flightSource = this.flight!.flightSource;
      this.order.extraData = this.flight!.extraData;
      parent.Book(this.flight!.idFlight, this.order);
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
  color: red;
}

.ticket-list {
  width: 100%;
  margin-bottom: 15px;
}
.input-item {
  margin: 5px;
}
</style>