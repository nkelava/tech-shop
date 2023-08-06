<script setup>
import { computed } from "vue";
import ProductCard from "@/components/ProductCard.vue";

const props = defineProps(["products"]);
// TODO: add slice count as prop if you dont think of another solution
const slicedProducts = computed(() => {
  return props.products.slice(0, 12);
});
</script>

<template>
  <v-row class="grid">
    <v-col
      cols="12"
      xs="12"
      sm="6"
      md="4"
      lg="3"
      xl="2"
      v-for="product in slicedProducts"
      :key="product.id"
      class="grid-item"
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
