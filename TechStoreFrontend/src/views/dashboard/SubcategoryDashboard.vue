<script setup>
import { onMounted, ref } from "vue";
import { axiosPrivate } from "@/api/axios";
import SubcategoryForm from "@/components/admin/subcategory/SubcategoryForm.vue";
import SubcategoryList from "@/components/admin/subcategory/SubcategoryList.vue";

const subcategories = ref([]);
const selectedSubcategoryId = ref(null);

onMounted(() => reloadSubcategories());

async function reloadSubcategories() {
  const resp = await axiosPrivate.get("/subcategories").catch((error) => console.log(error));

  if (resp.status !== 200) return;

  subcategories.value = resp.data;
}

function editSubcategory(id) {
  selectedSubcategoryId.value = id;
}
</script>

<template>
  <div>
    <subcategory-form
      :id="selectedSubcategoryId"
      @reload="reloadSubcategories"
      @clearSelectedId="selectedSubcategoryId = null"
    />
    <subcategory-list
      :subcategories="subcategories"
      @reload="reloadSubcategories"
      @edit="editSubcategory"
    />
  </div>
</template>
