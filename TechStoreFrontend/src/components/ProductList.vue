<script setup>
import ProductCard from "@/components/ProductCard.vue";
import { computed, ref } from "vue";

const props = defineProps(["products"]);
const pageState = ref({
  currentPage: 1,
  itemsPerPage: 12,
});

const totalPageCount = computed(() =>
  Math.ceil(props.products.length / pageState.value.itemsPerPage)
);

const currentPageItems = computed(() => {
  return props.products.slice(
    (pageState.value.currentPage - 1) * pageState.value.itemsPerPage,
    pageState.value.currentPage * pageState.value.itemsPerPage
  );
});
</script>

<template>
  <v-infinite-scroll height="300" mode="manual" @load="load">
    <div class="product-list">
      <product-card v-for="product in currentPageItems" :key="product" :product="product" />
      <v-container>
        <v-row justify="center">
          <v-col cols="10">
            <v-container class="max-width">
              <v-pagination v-model="pageState.currentPage" class="my-4" :length="totalPageCount" />
            </v-container>
          </v-col>
        </v-row>
      </v-container>
    </div>
  </v-infinite-scroll>
</template>

<style scoped>
.product-list {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 1rem;
}
</style>
