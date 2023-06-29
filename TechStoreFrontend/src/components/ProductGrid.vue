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
  <v-row justify="center" class="space">
    <v-col
      cols="12"
      xs="12"
      sm="6"
      md="4"
      lg="4"
      xl="2"
      v-for="product in slicedProducts"
      :key="product.id"
    >
      <product-card :product="product" />
    </v-col>
  </v-row>
</template>
