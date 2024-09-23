<script setup>
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import { axiosPublic } from "@/api/axios";
import ProductGallery from "@/components/product/ProductGallery.vue";
import ProductDetails from "@/components/product/ProductDetails.vue";
import TabsWrapper from "@/components/product/TabsWrapper.vue";

const route = useRoute();
const categorySlug = ref(route.params.category);
const subcategorySlug = ref(route.params.subcategory);
const productSlug = ref(route.params.productSlug);
const subcategoryDetails = ref({});
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
    title: subcategoryDetails.value.name || subcategorySlug.value,
    disabled: false,
    href: `/${categorySlug.value}/${subcategorySlug?.value}`,
  },
  {
    title: product.value.name || productSlug.value,
    disabled: true,
    href: `/${categorySlug.value}/${subcategorySlug.value}/${productSlug.value}`,
  },
];

const getProduct = async () => {
  await axiosPublic
    .get(`/products/${productSlug.value}`)
    .then((response) => (product.value = response.data))
    .catch((error) => console.log(error));
};

const updateProduct = async () => {
  await getProduct();
};

onMounted(async () => {
  await axiosPublic
    .get(`/subcategories/${subcategorySlug.value}`)
    .then((response) => (subcategoryDetails.value = response.data))
    .catch((error) => {
      console.error(`Failed to fetch subcategory details for ${subcategorySlug.value}:`, error);
    });

  await getProduct();
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
      <product-gallery class="gallery" :product="product" />
      <product-details class="info" :product="product" />
      <tabs-wrapper class="tabs" :product="product" :update="updateProduct" />
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
