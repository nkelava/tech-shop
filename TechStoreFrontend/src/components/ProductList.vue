<script setup>
import { computed } from "vue";
import ProductCard from "@/components/ProductCard.vue";

const props = defineProps(["products"]);
// TODO: add slice count as prop if you dont think of another solution
const slicedProducts = computed(() => {
  return props.products.slice(0, 8);
});
</script>

<template>
  <div class="products">
    <product-card v-for="product in slicedProducts" :key="product.id" :product="product" />
  </div>
</template>

<style scoped>
.products {
  display: grid;
  grid-template-columns: repeat(4, 200px);
  gap: 1rem;
  justify-content: center;
}
</style>
