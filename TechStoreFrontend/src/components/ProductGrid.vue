<script setup>
import { computed } from "vue";
import ProductCard from "@/components/ProductCard.vue";

const props = defineProps(["header", "products"]);
// TODO: add slice count as prop if you dont think of another solution
const slicedProducts = computed(() => {
  return props.products.slice(0, 10);
});
</script>

<template>
  <div class="grid-container py-15">
    <h2>{{ props.header }}</h2>
    <hr />
    <div class="grid__list2 w-100">
      <product-card v-for="product in slicedProducts" :key="product.id" :product="product" />
    </div>
  </div>
</template>

<style scoped>
.grid-container {
  max-width: 70%;
  margin: 0 auto;
}
.grid__list {
  display: grid;
  grid-template-columns: repeat(5, 1fr);
  gap: 1rem;
  justify-content: center;
  padding-top: 2rem;
}

.grid__list2 {
  display: grid;
  grid-auto-columns: max-content;
  grid-auto-flow: dense;
  grid-auto-rows: minmax(200px, auto);
  grid-template-columns: repeat(5, 1fr);
  grid-gap: 20px;

  margin: 50px 0;
}

hr {
  border: 1px solid var(--ts-c-primary-darker);
  border-radius: 5px;
  margin-bottom: 2rem;
}
</style>
