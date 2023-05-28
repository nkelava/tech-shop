<script setup>
import { useRoute } from "vue-router";
import { ref, watch } from "vue";
import { productsDb } from "@/data/products.js";
import ProductList from "@/components/ProductList.vue";

const route = useRoute();
const category = ref(route.params.category);
const subcategory = ref(route.params.subcategory);

watch(
  () => route.params.category,
  (newCategory) => {
    category.value = newCategory;
  }
);
</script>

<template>
  <div class="layout">
    <div class="sidebar">
      <div class="item">
        <h3>Price</h3>
        <hr />
        <input type="number" />
        <input type="number" />
      </div>
      <div class="item">
        <h3>Brand</h3>
        <hr />
        <div class="brands">
          <div>
            <input type="checkbox" id="lorem" />
            <label for="lorem">Lorem</label>
          </div>
          <div>
            <input type="checkbox" id="lorem" />
            <label for="lorem">Lorem</label>
          </div>
          <div>
            <input type="checkbox" id="lorem" />
            <label for="lorem">Lorem</label>
          </div>
          <div>
            <input type="checkbox" id="lorem" />
            <label for="lorem">Lorem</label>
          </div>
          <div>
            <input type="checkbox" id="lorem" />
            <label for="lorem">Lorem</label>
          </div>
          <div>
            <input type="checkbox" id="lorem" />
            <label for="lorem">Lorem</label>
          </div>
          <div>
            <input type="checkbox" id="lorem" />
            <label for="lorem">Lorem</label>
          </div>
        </div>
      </div>
      <div class="item">
        <h3>Rating</h3>
        <hr />
        <input type="range" min="0" max="5" />
      </div>
    </div>
    <div class="main">
      <h1>{{ category }} > {{ subcategory }}</h1>
      <hr />
      <ProductList :products="productsDb" />
    </div>
  </div>
</template>

<style scoped>
.layout {
  display: flex;
  justify-content: center;
  gap: 3rem;
  margin: 3rem 0;
}

h1 {
  text-transform: capitalize;
}

hr {
  margin-bottom: 1rem;
  margin-right: 2rem;
}

.sidebar {
  background-color: var(--ts-c-bg-light);
  border-radius: 10px;
  color: var(--ts-c-text-dark);
  width: 300px;
  display: flex;
  flex-direction: column;
  gap: 2rem;
}

.item {
  margin-inline: 20px;
  margin-top: 2rem;
}

.brands {
  display: flex;
  flex-direction: column;
  align-items: start;
  gap: 5px;
}

input[type="checkbox"] {
  margin-right: 10px;
}
</style>
