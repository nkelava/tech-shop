<script setup>
import TheSlider from "@/components/TheSlider.vue";
import TheBenefits from "@/components/TheBenefits.vue";
import TheBanners from "@/components/TheBanners.vue";
import ProductList from "@/components/ProductList.vue";
import ProductSlider from "@/components/ProductSlider.vue";
import { getProducts } from "@/database/services/productService";

const products = getProducts();
</script>

<template>
  <div>
    <TheSlider height="500px" />
    <TheBenefits />
    <div class="best-sellers">
      <!-- TODO: add product grid component and pagination to it -->
      <h2>Best Sellers</h2>
      <hr />
      <ProductList :products="products" />
    </div>
    <TheBanners />
    <!-- Add product slider component  -->
    <div class="new">
      <h2>New Arrivals</h2>
      <hr />
      <ProductSlider :products="products" />
    </div>
    <div class="hot">
      <h2>Hot Offers</h2>
      <hr />
      <ProductSlider :products="products" />
    </div>
  </div>
</template>

<style scoped>
.best-sellers {
  margin: 40px 0;
  padding: 0.5rem 5rem;
}

.best-sellers h2,
.new h2,
.hot h2 {
  color: var(--ts-c-text-highlight);
  padding: 3px;
}

hr {
  border: 1px solid var(--ts-c-primary-darker);
  border-radius: 5px;
  margin-bottom: 2rem;
}

.new,
.hot {
  margin: 40px 0;
  padding: 0.5rem 5rem;
}
</style>
