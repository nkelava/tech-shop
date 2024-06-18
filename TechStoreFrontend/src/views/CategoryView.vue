<script setup>
import { onMounted, ref, watch } from "vue";
import { useRoute } from "vue-router";
import { axiosPublic } from "@/api/axios";
import ImageSlider from "@/components/ImageSlider.vue";
import BaseGrid from "@/components/common/BaseGrid.vue";
import SubcategoryCard from "@/components/SubcategoryCard.vue";

const route = useRoute();
const categorySlug = ref(route.params.category);
const category = ref({});
const breadcrumbsItems = [
  {
    title: "Home",
    disabled: false,
    href: "/",
  },
  {
    title: `${categorySlug.value}`,
    disabled: true,
  },
];

const fetchCategoryData = async (slug) => {
  try {
    const response = await axiosPublic.get(`/categories/${slug}/subcategories`);
    category.value = response.data;
  } catch (error) {
    console.error(`Failed to fetch data for category ${slug}:`, error);
    category.value = null; // Set to null on error
  }
};

onMounted(async () => {
  await fetchCategoryData(categorySlug.value);
});

watch(
  () => route.params.category,
  async (newCategory) => {
    categorySlug.value = newCategory;
    await fetchCategoryData(categorySlug.value);
  }
);
</script>

<template>
  <div>
    <image-slider />

    <div class="ts-breadcrumbs">
      <v-breadcrumbs :items="breadcrumbsItems">
        <template v-slot:divider>
          <v-icon>mdi-chevron-right</v-icon>
        </template>
      </v-breadcrumbs>
    </div>

    <div class="ts-container">
      <h1 class="category__title text-capitalize">{{ category?.name || categorySlug }}</h1>
      <hr />
      <base-grid v-if="category?.subcategories?.length">
        <subcategory-card
          v-for="subcategory in category.subcategories"
          :key="subcategory.categoryId"
          :category="category"
          :subcategory="subcategory"
        />
      </base-grid>
      <p v-else>No subcategories available at the moment.</p>
    </div>
  </div>
</template>

<style scoped>
.category__list {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  justify-content: center;
  gap: 1rem;
}
</style>
