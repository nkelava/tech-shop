<script setup>
import AppProductCard from "@/components/AppProductCard.vue";

const { products } = defineProps(["products"]);
</script>

<template>
  <div class="products">
    <AppProductCard v-for="product in products" :key="product.id" :product="product" />
  </div>
</template>

<style scoped>
.products {
  display: grid;
  grid-template-columns: repeat(auto-fit, 250px);
  gap: 1rem;
  justify-content: center;
}
</style>
