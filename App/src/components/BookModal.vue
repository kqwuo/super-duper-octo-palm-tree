<template>
  <div class="modal-mask">
    <div class="modal-wrapper">
      <div class="modal-container">
        <div class="modal-body">
          <div style="display:flex;" v-for="ticket in this.order.ticketList" :value="ticket" :key="ticket">
            <p>Nom</p>
            <input v-model="ticket.firstName" />
            <br /><br />
            <p>Prénom</p>
            <input v-model="ticket.lastName" />
            <br /><br />
            <p>Type</p>
            <select v-model="ticket.userType">
              <option :value="this.userTypeEnum.adult"><p>Adulte</p></option>
              <option :value="this.userTypeEnum.child"><p>Enfant</p></option>
            </select>
            <input type="number" :value="ticket.nbAdditionalLuggage" min="0" max="3" />
          </div>
          <br><br>
          <button @click="this.order.ticketList.push(this.createTicket())">+</button>
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
              this.$parent.Book(idFlight, this.order);
              this.$emit('close');
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
import { Vue } from "vue-class-component";
import { Prop } from "vue-property-decorator";
import { Currency } from "../models/currency";
import { Order } from "../models/order";
import { Ticket } from "../models/ticket";
import { UserType } from "../models/usertype";

export default class BookModal extends Vue {
  @Prop() public idFlight?: any;
  order: Order = {
    user: {
      name: "",
    },
    nbBought: 0,
    ticketList: new Array<Ticket>(this.createTicket()),
    totalBasePrice: 0,
    totalAdditionalPrice: 0,
    totalDiscountedBasePrice: 0,
    totalPrice: 0,
    usedCurrency: Currency.EUR,
    exchangeRate: 0,
    isPaid: false,
  };

  userTypeEnum = UserType;

  createTicket(): Ticket {
    const ticket: Ticket = {
      nbAdditionalLuggage: 0,
      firstName: "",
      lastName: "",
      userType: UserType.adult,
      basePrice: 0,
      additionalPrice: 0,
      paidTotal: 0,
      basePriceDiscount: 0,
      discountedBasePrice: 0,
    };

    return ticket;
  }

  close() {
    this.$emit("close");
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
  width: 300px;
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
</style>