<script setup>
import { computed, ref } from "vue";
import { useCartStore } from "@/store";
import CartTable from "@/components/cart/CartTable.vue";
import OrderDialog from "@/components/order/OrderDialog.vue";
import CartIcon from "@/assets/icons/header/cart.png";
import EmptyStateImage from "@/assets/images/test/empty_state.png";
import { getPromoCode } from "@/database/services/promoCodeService.js";

const cart = useCartStore();
const cartDialogActive = ref(false);
const orderDialogActive = ref(false);
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
  orderDialogActive.value = !orderDialogActive.value;
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
    <v-btn class="widget__btn" variant="text" @click="cartDialogActive = !cartDialogActive">
      <v-badge :content="cart.itemCount">
        <img :src="CartIcon" alt="favorites icon" />
      </v-badge>
    </v-btn>
    <v-dialog v-model="cartDialogActive" persistent width="auto">
      <v-card class="dialog">
        <v-card-title class="font-weight-bold"> Your Shopping Cart </v-card-title>
        <v-card-text v-if="currentPageItems.length">
          <cart-table :products="currentPageItems" />
          <v-container>
            <v-row justify="center">
              <v-col cols="10">
                <v-container class="max-width">
                  <v-pagination
                    v-model="pageState.currentPage"
                    class="my-1"
                    :length="totalPageCount"
                  />
                </v-container>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>
        <div v-if="currentPageItems.length" class="price">
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
        <div v-else class="empty__container">
          <v-img class="empty__image" :src="EmptyStateImage">
            <template v-slot:placeholder>
              <div class="d-flex align-center justify-center fill-height">
                <v-progress-circular
                  :size="80"
                  color="teal-darken-2"
                  indeterminate
                ></v-progress-circular>
              </div>
            </template>
          </v-img>
          <p>Your Cart is empty!</p>
        </div>
        <v-card-actions class="justify-space-between mt-4">
          <v-btn color="red-darken-1" variant="text" @click="cartDialogActive = !cartDialogActive">
            Close
          </v-btn>
          <v-btn
            v-if="currentPageItems.length"
            class="checkout"
            variant="text"
            @click="orderDialogActive = !orderDialogActive"
          >
            Checkout
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <order-dialog v-model="orderDialogActive" @toggleDialog="toggleDialog" />
  </v-card>
</template>

<style scoped>
.dialog {
  padding: 1rem 0.5rem;
  min-width: 400px;
  background: var(--ts-c-bg-light);
}

.dialog * {
  color: var(--ts-c-text-dark);
}

.price {
  display: flex;
  flex-wrap: wrap;
  align-items: end;
}

.price h2 {
  margin: 1rem 0 0 auto;
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
  height: 100% !important;
  border: 1px solid transparent;
  border-radius: 0 5px 5px 0;
  background-color: var(--ts-c-bg-dark);
  color: var(--ts-c-text-light);
  height: 100%;
  padding: 5px 15px;
  text-transform: capitalize;
}

.checkout {
  text-decoration: none;
  color: var(--ts-c-success) !important;
}

.empty__container {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 1rem 0;
}

.empty__image {
  height: 200px;
  width: 200px;
}

.empty__container p {
  color: var(--ts-c-text-dark);
}
</style>
