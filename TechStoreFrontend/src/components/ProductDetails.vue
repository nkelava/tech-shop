<script setup>
import { computed, ref } from "vue";

const counter = ref(0);
const { product } = defineProps(["product"]);
const productRating = computed(() => {
  return "★★★★★☆☆☆☆☆".slice(Math.trunc(product.rating) - 1, Math.trunc(product.rating) + 4);
});

const increment = () => {
  counter.value += 1;
};

const decrement = () => {
  if (counter.value) {
    counter.value -= 1;
  }
};
</script>

<template>
  <section class="product__details">
    <h1>{{ product.title }}</h1>
    <p class="rating">{{ productRating }} {{ product.rating }} ({{ product.ratingCount }})</p>
    <p class="desc">{{ product.description }}</p>
    <h1 class="price">{{ product.price }} {{ product.currency }}</h1>
    <div class="quantity">
      <span>Quantity:</span>
      <span class="quantity__input">
        <button class="quantity__btn left" @click="decrement">-</button>
        <input type="number" :value="counter" />
        <button class="quantity__btn right" @click="increment">+</button>
      </span>
    </div>
    <button class="cart__btn">Add to cart</button>
  </section>
</template>

<style scoped>
.product__details {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  gap: 1rem;
  padding: 3rem;
  min-width: 0;
}

.rating,
.price {
  color: var(--ts-c-ternary);
}

.desc {
  color: var(--ts-c-secondary-dark);
}

.quantity {
  display: flex;
  align-items: center;
}

.quantity span {
  margin-right: 10px;
}

.quantity__input {
  position: relative;
  display: flex;
  align-items: center;
}

.quantity__btn {
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
  border: none;
  border-radius: 50%;
  height: 20px;
  width: 20px;
  position: absolute;
  outline: none;
  padding: 0;
}

.left {
  left: 5px;
}

.right {
  right: 5px;
}

input[type="number"] {
  border: none;
  border-radius: 30px;
  color: var(--ts-c-text-dark);
  outline: none;
  padding: 2px 5px;
  text-align: center;
  height: 25px;
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
  background-color: var(--ts-c-ternary);
  color: var(--ts-c-text-dark);
  border: none;
  border-radius: 30px;
  height: 3rem;
  width: 300px;
  font-weight: bold;
}
</style>
