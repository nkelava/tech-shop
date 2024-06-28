<script setup>
import { ref, computed, onMounted, watch } from "vue";
import { useRoute, useRouter } from "vue-router";
import { axiosPublic } from "@/api/axios";
import ProductList from "@/components/ProductList.vue";
import FilterSidebar from "@/components/TheFilterSidebar.vue";
import { parseProductAttributes } from "@/helpers/product";
import { filterProducts } from "@/helpers/filter";

const route = useRoute();
const categorySlug = ref(route.params.category);
const subcategorySlug = ref(route.params.subcategory);
const subcategoryDetails = ref({});
const products = ref([]);
const filteredProducts = ref([]);
const attributeValuesMap = ref();
const sortType = ref("");

const breadcrumbsItems = computed(() => [
  {
    title: "Home",
    disabled: false,
    href: "/",
  },
  {
    title: categorySlug.value,
    disabled: false,
    href: `/${categorySlug.value}`,
  },
  {
    title: subcategoryDetails.value.name || subcategorySlug.value,
    disabled: true,
  },
]);

onMounted(async () => {
  await axiosPublic
    .get(`/subcategories/${subcategorySlug.value}`)
    .then((response) => (subcategoryDetails.value = response.data))
    .catch((error) => {
      console.error(`Failed to fetch subcategory details for ${slug}:`, error);
    });

  await axiosPublic
    .get(`/products/subcategory/${subcategorySlug.value}`)
    .then((response) => {
      products.value = response.data;
      filteredProducts.value = products.value;
      attributeValuesMap.value = parseProductAttributes(products.value);
    })
    .catch((error) => {
      console.error(`Failed to fetch products for subcategory ${slug}:`, error);
    });
});

const updateSortType = (event) => {
  sortType.value = event.target.value;
};

const sortedProducts = computed(() => {
  const sortedProducts = [...filteredProducts.value];

  switch (sortType.value) {
    case "low":
      return sortedProducts.sort((a, b) => a.price - b.price);
    case "high":
      return sortedProducts.sort((a, b) => a.price - b.price).reverse();
    case "asc":
      return sortedProducts.sort((a, b) => a.name.localeCompare(b.name));
    case "desc":
      return sortedProducts.sort((a, b) => a.name.localeCompare(b.name)).reverse();
    default:
      return sortedProducts;
  }
});

const handleFilterProducts = (price, rating, filters) => {
  filteredProducts.value = filterProducts(products.value, price, rating, filters);
};
</script>

<template>
  <div>
    <div class="ts-breadcrumbs">
      <v-breadcrumbs :items="breadcrumbsItems">
        <template v-slot:divider>
          <v-icon>mdi-chevron-right</v-icon>
        </template>
      </v-breadcrumbs>
    </div>

    <div class="sidebar-layout ts-container">
      <filter-sidebar
        class="sidebar"
        :attributeValuesMap="attributeValuesMap"
        @filter="handleFilterProducts"
      />
      <div class="main">
        <div class="heading">
          <h1 class="heading__title">{{ subcategoryDetails?.name || subcategorySlug }}</h1>
          <!-- TODO: create sort select component -->
          <select class="heading__sort" name="sort" @change="updateSortType">
            <option value="" hidden>Sort...</option>
            <option value="asc">Alphabetically: A-Z</option>
            <option value="desc">Alphabetically: Z-A</option>
            <option value="low">Price: Low to High</option>
            <option value="high">Price: High to Low</option>
          </select>
        </div>
        <hr />
        <product-list v-if="products.length" :products="sortedProducts" />
        <p v-else>No products available at the moment.</p>
      </div>
    </div>
  </div>
</template>

<style scoped>
.sidebar-layout {
  display: grid;
  grid-template-areas: "sidebar main";
  grid-template-columns: minmax(300px, 350px) auto;
  align-items: flex-start;
  gap: 100px;
}

.sidebar {
  grid-area: sidebar;
}

.main {
  grid-area: main;
}

.heading {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  padding-bottom: 5px;
}

.heading__title {
  text-transform: capitalize;
}

.heading__sort {
  height: 30px;
  padding: 5px 20px 5px 10px;
  align-self: end;
  background-color: var(--ts-c-bg-light);
  border: none;
  border-radius: 5px;
  color: var(--ts-c-text-dark);
}

@media only screen and (max-width: 1024px) {
  .sidebar-layout {
    grid-template-areas:
      "sidebar"
      "main";
    grid-template-columns: auto;
    gap: 1rem;
  }

  .sidebar {
    min-width: 100%;
  }

  .main {
    min-width: 100%;
  }
}
</style>
