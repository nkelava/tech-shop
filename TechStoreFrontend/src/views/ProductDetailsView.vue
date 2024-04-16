<script setup>
import { computed, onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import axios from "@/api/axios";
import ProductGallery from "@/components/ProductGallery.vue";
import ProductDetails from "@/components/ProductDetails.vue";
import TabsWrapper from "@/components/TabsWrapper.vue";

const route = useRoute();
const categorySlug = ref(route.params.category);
const subcategorySlug = ref(route.params.subcategory);
const productSlug = ref(route.params.productSlug);
const product = ref({});
const breadcrumbsItems = [
  {
    title: "Home",
    disabled: false,
    href: "/",
  },
  {
    title: `${categorySlug.value}`,
    disabled: false,
    href: `/${categorySlug.value}`,
  },
  {
    title: `${subcategorySlug.value}`,
    disabled: false,
    href: `/${categorySlug.value}/${subcategorySlug.value}`,
  },
  {
    title: `${productSlug.value}`,
    disabled: true,
    href: `/${categorySlug.value}/${subcategorySlug.value}/${productSlug.value}`,
  },
];

onMounted(async () => {
  await axios
    .get(`/products/${productSlug.value}`)
    .then((response) => (product.value = response.data))
    .catch((error) => console.log(error));
});
</script>

<template>
  <div class="ts-breadcrumbs">
    <v-breadcrumbs :items="breadcrumbsItems">
      <template v-slot:divider>
        <v-icon>mdi-chevron-right</v-icon>
      </template>
    </v-breadcrumbs>
    <div class="product-container ts-container">
      <product-gallery class="gallery" />
      <product-details class="info" :product="product" />
      <tabs-wrapper class="tabs" :product="product" />
    </div>
  </div>
</template>

<style scoped>
.product-container {
  display: grid;
  grid-template-areas:
    "gallery info"
    "tabs tabs";
  grid-template-columns: 0.9fr 1fr;
  max-width: 100%;
}

.gallery {
  grid-area: gallery;
}

.info {
  grid-area: info;
}

.tabs {
  grid-area: tabs;
  margin-top: 5rem;
}

@media (max-width: 1000px) {
  .product-container {
    grid-template-areas:
      "gallery"
      "info"
      "tabs";

    grid-template-columns: 1fr;
  }
}

@media (min-width: 1450px) {
  .tabs {
    width: 75%;
    margin-inline: auto;
  }
}
</style>
