<script setup>
import { computed } from "vue";
import ProductCard from "@/components/ProductCard.vue";

const props = defineProps(["products"]);
// TODO: add slice count as prop if you dont think of another solution
const slicedProducts = computed(() => {
  return props.products.slice(0, 10);
});
</script>

<template>
  <div class="grid__list">
    <product-card v-for="product in slicedProducts" :key="product.id" :product="product" />
  </div>
</template>

<style scoped>
.grid__list {
  display: grid;
  grid-auto-columns: max-content;
  grid-auto-flow: dense;
  grid-auto-rows: minmax(250px, auto);
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  grid-gap: 2rem;
  width: 100%;
}
</style>
