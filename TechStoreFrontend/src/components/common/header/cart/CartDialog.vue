<script setup>
import { computed, ref } from "vue";
import { useCartStore } from "@/stores";
import CartTable from "./CartTable.vue";
import OrderDialog from "@/components/OrderDialog.vue";
import { getPromoCode } from "@/database/services/promoCodeService.js";
import CartIcon from "@/assets/icons/header/cart.png";

const cart = useCartStore();
const cartDialog = ref(false);
const orderDialog = ref(false);
const promoCodeState = ref({
  input: "",
  isActive: false,
  discount: 0,
});
const pageState = ref({
  currentPage: 1,
  itemsPerPage: 3,
});

const totalPageCount = computed(() => Math.ceil(cart.items.length / pageState.value.itemsPerPage));
const currentPageItems = computed(() => {
  return cart.items.slice(
    (pageState.value.currentPage - 1) * pageState.value.itemsPerPage,
    pageState.value.currentPage * pageState.value.itemsPerPage
  );
});

function toggleDialog() {
  orderDialog.value = !orderDialog.value;
}

function deleteItem(productId) {
  cart.removeItem(productId);
}

function addPromoCode() {
  if (promoCodeState.value.input < 1) return;

  const promoCode = getPromoCode(promoCodeState.value.input);

  // Check promo code
  // If false display error
  if (!promoCode) return;
  // Else apply promo code and display success message
  promoCodeState.value.discount = promoCode.discount;
  promoCodeState.value.isActive = true;
}

function removePromoCode() {
  promoCodeState.value.discount = 0;
  promoCodeState.value.isActive = false;
}
</script>

<template>
  <v-card variant="text">
    <v-btn class="pa-1" variant="text" @click="cartDialog = !cartDialog">
      <v-badge :content="cart.itemCount" color="var(--ts-c-primary-mute)">
        <img :src="CartIcon" alt="favorites icon" class="dropdown__icon" />
      </v-badge>
    </v-btn>
    <v-dialog v-model="cartDialog" persistent width="auto">
      <v-card class="dialog">
        <v-card-title> Your Shopping Cart</v-card-title>
        <v-card-text>
          <cart-table :products="currentPageItems" @deleteItem="deleteItem" />
          <v-container>
            <v-row justify="center">
              <v-col cols="10">
                <v-container class="max-width">
                  <v-pagination
                    v-model="pageState.currentPage"
                    class="my-4"
                    :length="totalPageCount"
                  />
                </v-container>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>
        <div class="price">
          <form @submit.prevent>
            <input
              v-model="promoCodeState.input"
              class="promo-code"
              type="text"
              placeholder="Promo Code"
              :disabled="promoCodeState.isActive"
              required
            />
            <input
              v-if="!promoCodeState.isActive"
              class="btn-submit"
              type="submit"
              value="Add"
              @click="addPromoCode"
            />
            <button v-if="promoCodeState.isActive" class="btn-submit" @click="removePromoCode">
              Remove
            </button>
          </form>
          <h2 class="text-end pr-4">Total: {{ cart.totalPrice(promoCodeState.discount) }}$</h2>
        </div>
        <v-card-actions class="justify-space-between">
          <v-btn color="red-darken-1" variant="text" @click="cartDialog = !cartDialog">
            Close
          </v-btn>
          <v-btn color="green-darken-1" variant="text" @click="orderDialog = !orderDialog">
            Checkout
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <OrderDialog v-model="orderDialog" @toggleDialog="toggleDialog" />
  </v-card>
</template>

<style scoped>
.dialog {
  background: var(--ts-c-bg-light);
}

.dialog * {
  color: var(--ts-c-text-dark);
}

.price {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.promo-code {
  border: 1px solid var(--ts-c-bg-dark);
  border-radius: 5px 0 0 5px;
  color: var(--ts-c-text-dark);
  outline: none;
  max-width: 200px !important;
  margin-left: 1rem;
  padding: 5px 10px;
}

.btn-submit {
  height: 33px !important;
  border: 1px solid transparent;
  border-radius: 0 5px 5px 0;
  background-color: var(--ts-c-bg-dark);
  color: var(--ts-c-text-light);
  height: 100%;
  padding: 5px 10px;
  text-transform: capitalize;
}

.checkout {
  color: green;
  text-decoration: none;
}
</style>
