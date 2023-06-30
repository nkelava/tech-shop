<script setup>
import { computed, ref } from "vue";
import { RouterLink } from "vue-router";
import { getProducts } from "@/database/services/productService.js";
import { getPromoCode } from "@/database/services/promoCodeService.js";
import CartIcon from "@/assets/icons/header/cart.png";
import CartTable from "./CartTable.vue";

const promoCodeInput = ref("");
const isPromoCodeActive = ref(false);
const promoCodeDiscount = ref(0);
const dialog = ref(false);
const products = ref(getProducts());
const page = ref(1);
const pageItems = 3;
const pageCount = ref(Math.ceil(products.value.length / pageItems));

const getProductCount = computed(() => products.value.length);
const getPageProducts = computed(() => {
  return products.value.slice((page.value - 1) * pageItems, page.value * pageItems);
});
const getTotal = computed(() => {
  const totalPrice = products.value
    .reduce((total, product) => (total += product.price), 0)
    .toFixed(2);
  const discount = totalPrice * (promoCodeDiscount.value / 100).toFixed(2);
  return totalPrice - discount;
});

function toggleDialog() {
  dialog.value = !dialog.value;
}

function deleteItem(productId) {
  const filteredProducts = products.value.filter((product) => product.id != productId);
  products.value = filteredProducts;
  pageCount.value = Math.ceil(products.value.length / pageItems);
}

function addPromoCode() {
  if (promoCodeInput.value < 1) return;

  const promoCode = getPromoCode(promoCodeInput.value);

  // Check promo code
  // If false display error
  if (!promoCode) return;
  // Else apply promo code and display success message
  promoCodeDiscount.value = promoCode.discount;
  isPromoCodeActive.value = true;
}

function removePromoCode() {
  promoCodeDiscount.value = 0;
  isPromoCodeActive.value = false;
}
</script>

<template>
  <v-card variant="text">
    <v-btn class="pa-1" variant="text" @click="toggleDialog">
      <v-badge :content="getProductCount" color="var(--ts-c-primary-mute)">
        <img :src="CartIcon" alt="favorites icon" class="dropdown__icon" />
      </v-badge>
    </v-btn>
    <v-dialog v-model="dialog" persistent width="auto">
      <v-card class="dialog">
        <v-card-title> Your Shopping Cart</v-card-title>
        <v-card-text>
          <cart-table :products="getPageProducts" @deleteItem="deleteItem" />
          <v-container>
            <v-row justify="center">
              <v-col cols="10">
                <v-container class="max-width">
                  <v-pagination v-model="page" class="my-4" :length="pageCount" />
                </v-container>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>
        <div class="price">
          <form @submit.prevent>
            <input
              v-model="promoCodeInput"
              class="promo-code"
              type="text"
              placeholder="Promo Code"
              :disabled="isPromoCodeActive"
              required
            />
            <input
              v-if="!isPromoCodeActive"
              class="btn-submit"
              type="submit"
              value="Add"
              @click="addPromoCode"
            />
            <button v-if="isPromoCodeActive" class="btn-submit" @click="removePromoCode">
              Remove
            </button>
          </form>
          <h2 class="text-end pr-4">Total: {{ getTotal }}$</h2>
        </div>
        <v-card-actions class="justify-space-between">
          <v-btn color="red-darken-1" variant="text" @click="toggleDialog"> Close </v-btn>
          <v-btn color="green-darken-1" variant="text" @click="toggleDialog">
            <router-link to="/order" class="checkout">Checkout</router-link>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
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
