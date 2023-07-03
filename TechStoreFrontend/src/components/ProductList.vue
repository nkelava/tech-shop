<script setup>
import ProductCard from "@/components/ProductCard.vue";

const props = defineProps(["products"]);
</script>

<template>
  <div class="product-list">
    <product-card v-for="product in props.products" :key="product.id" :product="product" />
  </div>
</template>

<style scoped>
.product-list {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 1rem;
}
</style>
