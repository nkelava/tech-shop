<script setup>
import { computed, ref } from "vue";
import { getProducts } from "@/database/services/productService";
import Helios from "@/assets/images/test/products/helios300.png";

const products = ref(getProducts().slice(0, 11));
const props = defineProps(["title"]);
const emit = defineEmits(["toggleDialog"]);
const page = ref(1);
const pageSize = 3;
const pages = ref(Math.ceil(products.value.length / pageSize));
const handleToggle = () => {
  emit("toggleDialog");
};

const getPageProducts = computed(() => {
  return products.value.slice((page.value - 1) * pageSize, page.value * pageSize);
});

const handleDelete = (productId) => {
  const filteredProducts = products.value.filter((product) => product.id != productId);
  products.value = filteredProducts;
  pages.value = Math.ceil(products.value.length / pageSize);
};
</script>

<template>
  <v-card class="dialog">
    <v-card-title> {{ props.title }} </v-card-title>
    <v-card-text>
      <v-table class="test">
        <thead>
          <tr>
            <th class="text-left"></th>
            <th class="text-left">Product</th>
            <th class="text-left">Price</th>
            <th class="text-left">Quantity</th>
            <th class="text-left">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="product in getPageProducts" :key="product.id">
            <td class="py-2">
              <img :src="Helios" class="border rounded-lg" width="150" height="150" />
            </td>
            <td>{{ product.name }}</td>
            <td>{{ product.price }}$</td>
            <td>0</td>
            <td>
              <!-- TODO: implement adding to cart -->
              <v-btn
                icon="mdi-plus"
                color="green"
                variant="text"
                title="Add to Cart"
                @click="addToCart(product.id)"
              ></v-btn>
              <!-- TODO: implement favorites item deletion -->
              <v-btn
                icon="mdi-delete"
                color="red"
                variant="text"
                title="Delete item"
                @click="handleDelete(product.id)"
              ></v-btn>
            </td>
          </tr>
        </tbody>
      </v-table>
      <v-container>
        <v-row justify="center">
          <v-col cols="10">
            <v-container class="max-width">
              <v-pagination v-model="page" class="my-4" :length="pages"></v-pagination>
            </v-container>
          </v-col>
        </v-row>
      </v-container>
    </v-card-text>
    <v-card-actions>
      <v-btn color="red-darken-1" variant="text" @click="handleToggle"> Close </v-btn>
    </v-card-actions>
  </v-card>
</template>

<style scoped>
.dialog {
  background: var(--ts-c-bg-light);
}

.dialog * {
  color: var(--ts-c-text-dark);
}
.test {
  background: var(--ts-c-bg-light);
}
</style>
