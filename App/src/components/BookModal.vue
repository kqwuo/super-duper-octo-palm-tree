<template>
  <div class="modal-mask">
    <div class="modal-wrapper">
      <div class="modal-container">
        <div class="modal-body">
          <div class="summary">
            Nom de l'acheteur : <input v-model="this.order.user.name" type="text" />
            Prix total : // //
          </div>
          <table class="ticket-list">
            <tr>
              <th>Nom</th>
              <th>Prénom</th>
              <th>Type</th>
              <th>Baggages supplémentaires</th>
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
              <td>
                <input
                  type="number"
                  :value="ticket.nbAdditionalLuggage"
                  min="0"
                  max="3"
                />
              </td>
              <td>
                // //
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
          <button @click="this.order.ticketList.push(this.createTicket())">
            +
          </button>
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
              this.order = this.createDefault();
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
  order: Order = this.createDefault();

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

  createDefault(): Order {
    return {
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