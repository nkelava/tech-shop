<script setup>
import { onMounted, ref } from "vue";
import { axiosPrivate } from "@/api/axios";
import CategoryForm from "@/components/admin/category/CategoryForm.vue";
import CategoryList from "@/components/admin/category/CategoryList.vue";

const categories = ref([]);

onMounted(() => reloadCategories());

async function reloadCategories() {
  const resp = await axiosPrivate.get("/categories").catch((error) => console.log(error));

  if (resp.status !== 200) return;

  categories.value = resp.data;
}

console.log("categories: ", categories.value);
</script>

<template>
  <div>
    <category-form @reload="reloadCategories" />
    <category-list @reload="reloadCategories" :categories="categories" />
  </div>
</template>
