<template>
    <div class="modal-mask">
      <div class="modal-wrapper">
        <div class="modal-container">
          <div class="modal-body">
            <slot name="body">
              <p>Nom</p>
              <input v-model="surname"/>
              <br><br>
              <p>Prénom</p>
              <input v-model="name"/>
              <br><br>
              <p>Type</p>
              <select v-model="type">
                <option><p>Adulte</p></option>
                <option><p>Enfant</p></option>
              </select>
            </slot>
          </div>

          <div class="modal-footer">
            <slot name="footer">
              <button type="button" @click="this.$parent.Book(idFlight); this.$emit('close')" class="btn btn-info">Valider</button>
            </slot>
          </div>
        </div>
      </div>
    </div>
</template>

<script lang="ts">
import { Vue } from "vue-class-component";
import { Prop } from 'vue-property-decorator';

export default class BookModal extends Vue {

  @Prop() public idFlight?: any;

  close() {
    this.$emit('close');
  }
};
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