<script setup>
import { ref, toRefs } from "vue";
import ProductSpecification from "@/components/product/ProductSpecification.vue";
import ProductReviews from "@/components/product/ProductReviews.vue";

const props = defineProps(["product", "update"]);
const { product } = toRefs(props);
const tab = ref();
</script>

<template>
  <v-card>
    <v-tabs class="tabs" v-model="tab" fixed-tabs>
      <v-tab value="specs"> Specification </v-tab>
      <v-tab value="reviews"> Reviews ({{ product.reviewCount }}) </v-tab>
    </v-tabs>

    <v-card-text class="text">
      <v-window v-model="tab">
        <v-window-item value="specs">
          <product-specification :product="product" />
        </v-window-item>

        <v-window-item value="reviews">
          <product-reviews :product="product" :update="props.update" />
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
