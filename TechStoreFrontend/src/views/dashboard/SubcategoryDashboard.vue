<script setup>
import { onMounted, ref } from "vue";
import { axiosPrivate } from "@/api/axios";
import SubcategoryForm from "@/components/admin/subcategory/SubcategoryForm.vue";
import SubcategoryList from "@/components/admin/subcategory/SubcategoryList.vue";

const subcategories = ref([]);

onMounted(() => reloadSubcategories());

async function reloadSubcategories() {
  const resp = await axiosPrivate.get("/subcategories").catch((error) => console.log(error));

  if (resp.status !== 200) return;

  subcategories.value = resp.data;
}
</script>

<template>
  <div>
    <subcategory-form @reload="reloadSubcategories" />
    <subcategory-list @reload="reloadSubcategories" :subcategories="subcategories" />
  </div>
</template>
