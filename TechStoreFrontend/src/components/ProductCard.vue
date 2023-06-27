<script setup>
import { useRouter } from "vue-router";

const props = defineProps(["product"]);
const category = "laptops";
const subcategory = "notebooks";
const router = useRouter();

function handleDetails() {
  router.push({
    name: "product",
    params: { category: category, subcategory: subcategory, productId: props.product.id },
  });
}
</script>

<template>
  <!-- TODO: add favorites and cart icon on hover -->
  <!-- TODO: add currency as separate property -->
  <v-card class="card" elevation="8">
    <v-card-title class="font-weight-bold">
      <img class="card__image" :src="props.product.img" />
    </v-card-title>
    <v-card-text class="pa-2">
      <h4 class="truncate" :title="props.product.title">{{ props.product.title }}</h4>
      <h3 class="card__price">{{ props.product.currency + props.product.price }}</h3>
    </v-card-text>
    <v-card-actions>
      <v-btn class="card__btn" @click="handleDetails">View More</v-btn>
    </v-card-actions>
  </v-card>
</template>

<style scoped>
.card {
  background-color: var(--ts-c-bg-light);
  border-radius: 15px;
  color: var(--ts-c-text-dark);
  text-align: center;
  max-width: 250px;
}

.card__image {
  max-height: 200px;
  width: 100%;
  object-fit: contain;
}

.card__price {
  margin-top: 30px;
}

.card__btn {
  background-color: var(--ts-c-bg-dark);
  border-radius: 10px;
  color: var(--ts-c-text-light);
  font-weight: bold;
  width: 100%;
  text-transform: capitalize;
}

.card__btn:hover {
  background-color: var(--ts-c-primary-soft);
}

.truncate {
  display: block;
  overflow: hidden;
  max-height: 2.6em;
  line-height: 1.3em;
}
</style>
