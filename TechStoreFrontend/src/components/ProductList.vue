<script setup>
import ProductCard from "@/components/ProductCard.vue";
import { computed } from "vue";

const props = defineProps(["products"]);
// TODO: add slice count as prop if you dont think of another solution
const slicedProducts = computed(() => {
  return props.products.slice(0, 8);
});
</script>

<template>
  <div class="products">
    <ProductCard v-for="product in slicedProducts" :key="product.id" :product="product" />
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
