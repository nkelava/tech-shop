<script setup>
import { onMounted, ref } from "vue";
import { axiosPrivate } from "@/api/axios";
import ProductForm from "@/components/admin/product/ProductForm.vue";
import ProductList from "@/components/admin/product/ProductList.vue";

const products = ref([]);
const selectedProductId = ref(null);

onMounted(() => reloadProducts());

async function reloadProducts() {
  const resp = await axiosPrivate.get("/products").catch((error) => console.log(error));

  if (resp?.status !== 200) return;

  products.value = resp.data;
}
function editProduct(id) {
  selectedProductId.value = id;
}
</script>

<template>
  <div>
    <product-form
      :id="selectedProductId"
      @reload="reloadProducts"
      @clearSelectedId="selectedProductId = null"
    />
    <product-list :products="products" @reload="reloadProducts" @edit="editProduct" />
  </div>
</template>
