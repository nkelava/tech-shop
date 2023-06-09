<script setup>
import { ref } from "vue";
import ProductSpecification from "./ProductSpecification.vue";
import ProductReviews from "./ProductReviews.vue";

const tab = ref();
const props = defineProps(["product"]);
const reviewCount = 3;
</script>

<template>
  <v-card>
    <v-tabs class="tabs" v-model="tab" fixed-tabs>
      <v-tab value="specs"> Specification </v-tab>
      <v-tab value="reviews"> Reviews ({{ reviewCount }}) </v-tab>
    </v-tabs>

    <v-card-text class="text">
      <v-window v-model="tab">
        <v-window-item value="specs">
          <ProductSpecification :product="props.product" />
        </v-window-item>

        <v-window-item value="reviews">
          <ProductReviews />
        </v-window-item>
      </v-window>
    </v-card-text>
  </v-card>
</template>

<style scoped>
.tabs {
  background-color: var(--ts-c-ternary-soft);
  color: var(--ts-c-text-dark);
}

.text {
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
}

.v-slide-group-item--active {
  background: var(--ts-c-ternary);
}

.v-btn {
  font-weight: bold;
}
</style>
