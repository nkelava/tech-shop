<script setup>
import ProductCard from "@/components/ProductCard.vue";

const { products } = defineProps(["products"]);
const productList = products.slice(0, 8);
</script>

<template>
  <div class="products">
    <ProductCard v-for="product in productList" :key="product.id" :product="product" />
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
