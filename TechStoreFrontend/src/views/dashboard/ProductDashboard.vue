<script setup>
import { onMounted, ref } from "vue";
import { axiosPrivate } from "@/api/axios";
import ProductForm from "@/components/admin/product/ProductForm.vue";
import ProductList from "@/components/admin/product/ProductList.vue";

const products = ref([]);

onMounted(() => reloadProducts());

async function reloadProducts() {
  const resp = await axiosPrivate.get("/products").catch((error) => console.log(error));

  if (resp.status !== 200) return;

  products.value = resp.data;
}
</script>

<template>
  <div>
    <product-form @reload="reloadProducts" />
    <product-list @reload="reloadProducts" :products="products" />
  </div>
</template>
