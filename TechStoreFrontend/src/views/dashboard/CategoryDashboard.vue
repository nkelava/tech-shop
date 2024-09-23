<script setup>
import { onMounted, ref } from "vue";
import { axiosPrivate } from "@/api/axios";
import CategoryForm from "@/components/admin/category/CategoryForm.vue";
import CategoryList from "@/components/admin/category/CategoryList.vue";

const categories = ref([]);
const selectedCategoryId = ref(null);

onMounted(() => reloadCategories());

async function reloadCategories() {
  const resp = await axiosPrivate.get("/categories").catch((error) => console.log(error));

  if (resp?.status !== 200) return;

  categories.value = resp.data;
}

function editCategory(id) {
  selectedCategoryId.value = id;
}
</script>

<template>
  <div>
    <category-form
      :id="selectedCategoryId"
      @reload="reloadCategories"
      @clearSelectedId="selectedCategoryId = null"
    />
    <category-list :categories="categories" @reload="reloadCategories" @edit="editCategory" />
  </div>
</template>
