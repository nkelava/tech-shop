<script setup>
import ProductCard from "@/components/ProductCard.vue";

const { products } = defineProps(["products"]);
</script>

<template>
  <div class="products">
    <ProductCard v-for="product in products" :key="product.id" :product="product" />
  </div>
</template>

<style scoped>
.products {
  display: grid;
  grid-template-columns: repeat(auto-fit, 200px);
  gap: 1rem;
  justify-content: center;
}
</style>
