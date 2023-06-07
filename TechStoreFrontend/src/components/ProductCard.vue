<script setup>
import { RouterLink } from "vue-router";

const { product } = defineProps(["product"]);
const category = "laptops";
const subcategory = "notebooks";
</script>

<template>
  <!-- TODO: add favorites and cart icon on hover -->
  <div class="card">
    <img :src="product.img" alt="" class="card__image" />
    <h2 class="card__title">{{ product.name }}</h2>
    <p class="card__summary">{{ product.summary }}</p>
    <!-- TODO: add currency as separate property -->
    <h3 class="card__price">{{ product.price + product.currency }}</h3>
    <button>
      <router-link
        :to="{
          name: 'product',
          params: { category: category, subcategory: subcategory, productId: product.id },
        }"
        >VIEW MORE
      </router-link>
    </button>
  </div>
</template>

<style scoped>
.card {
  background-color: var(--ts-c-bg-light);
  border-radius: 10px;
  box-shadow: 7px 5px 5px var(--ts-c-primary-darker);
  color: var(--ts-c-text-dark);
  padding: 1rem;
  text-align: center;
  width: 200px;
}

.card__image {
  width: 150px;
  height: 150px;
}

.card__title {
  font-size: 1rem;
  margin: 0.5rem;
}

.card__summary {
  font-size: 12px;
  color: var(--ts-c-secondary-darker);
}

.card__price {
  margin: 1rem 0;
}

.card button {
  background-color: var(--ts-c-bg-dark);
  border: none;
  border-radius: 30px;
  color: var(--ts-c-text-light);
  font-weight: bold;
  padding: 0.5rem;
  width: 10rem;
}
</style>
