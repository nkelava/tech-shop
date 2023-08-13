<script setup>
import { ref, toRefs } from "vue";
import { useCartStore } from "@/store";

const props = defineProps(["product"]);
const { product } = toRefs(props);
const counter = ref(1);
const cart = useCartStore();

const increment = () => {
  counter.value += 1;
};

const decrement = () => {
  if (counter.value > 1) {
    counter.value -= 1;
  }
};

function addToCart() {
  if (counter.value) {
    cart.addItem(product.value);
  }
}
</script>

<template>
  <section class="product__details">
    <h1>{{ product.title }}</h1>
    <span class="rating">
      <v-rating v-model="product.rating" size="small" density="compact" readonly> </v-rating>
      {{ product.rating }}
      ({{ product.ratingCount }})
    </span>
    <p class="description">{{ product.description }}</p>
    <h1 class="price">{{ product.price }} {{ product.currency }}</h1>
    <div class="quantity-container">
      <span class="quantity__label">Quantity:</span>
      <span class="quantity__input">
        <button class="quantity__btn quantity__btn--left" @click="decrement">-</button>
        <input v-model="counter" type="number" />
        <button class="quantity__btn quantity__btn--right" @click="increment">+</button>
      </span>
    </div>
    <button class="cart__btn" @click="addToCart">Add to Cart</button>
  </section>
</template>

<style scoped>
.product__details {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  gap: 1rem;
  min-width: 0;
}

.rating,
.price {
  color: var(--ts-c-ternary);
}

.rating {
  display: flex;
  align-items: center;
  gap: 10px;
  font-size: 16px;
}

.price {
  font-size: 22px;
}

.description {
  color: var(--ts-c-secondary-dark);
}

.quantity-container {
  display: flex;
  justify-content: flex-start;
  align-items: center;
}

.quantity__label {
  margin-right: 10px;
}

.quantity__input {
  position: relative;
  display: flex;
  align-items: center;
}

.quantity__btn {
  position: absolute;
  display: flex;
  justify-content: center;
  align-items: center;
  height: 20px;
  width: 20px;
  padding: 0;
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
  border: none;
  border-radius: 5px;
  outline: none;
}
.quantity__btn--left {
  left: 5px;
}

.quantity__btn--right {
  right: 5px;
}

input[type="number"] {
  height: 25px;
  padding: 2px 5px;
  text-align: center;
  background-color: #fff;
  color: var(--ts-c-text-dark);
  border: none;
  border-radius: 5px;
  outline: none;
}

/* Chrome, Safari, Edge, Opera */
input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

/* Firefox */
input[type="number"] {
  -moz-appearance: textfield;
}

.cart__btn {
  height: 3rem;
  width: 100%;
  max-width: 300;
  font-weight: bold;
  background-color: var(--ts-c-ternary);
  color: var(--ts-c-text-dark);
  border: none;
  border-radius: 5px;
}
</style>
