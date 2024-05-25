<script setup>
import { onMounted, ref } from "vue";
import { axiosPrivate } from "@/api/axios";
import AttributeForm from "@/components/admin/attribute/AttributeForm.vue";
import AttributeList from "@/components/admin/attribute/AttributeList.vue";

const attributes = ref([]);

onMounted(() => reloadAttributes());

async function reloadAttributes() {
  const resp = await axiosPrivate.get("/attributes").catch((error) => console.log(error));

  if (resp.status !== 200) return;

  attributes.value = resp.data;
}
</script>

<template>
  <div>
    <attribute-form @reload="reloadAttributes" />
    <attribute-list @reload="reloadAttributes" :attributes="attributes" />
  </div>
</template>
