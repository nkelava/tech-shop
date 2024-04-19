<script setup>
import ProductCard from "@/components/ProductCard.vue";

const props = defineProps(["products"]);
// TODO: add slice count as prop if you dont think of another solution
</script>

<template>
  <v-row class="grid">
    <v-col
      v-for="product in props.products"
      :key="product.id"
      class="grid-item"
      cols="12"
      xs="12"
      sm="6"
      md="4"
      lg="3"
      xl="2"
    >
      <product-card :product="product" />
    </v-col>
  </v-row>
</template>

<style scoped>
.grid-item {
  display: flex;
  justify-content: center;
}
</style>
