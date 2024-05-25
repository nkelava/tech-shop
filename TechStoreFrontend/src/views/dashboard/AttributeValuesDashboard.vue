<script setup>
import { onMounted, ref } from "vue";
import { axiosPrivate } from "@/api/axios";
import AttributeValuesForm from "@/components/admin/attribute_values/AttributeValuesForm.vue";
import AttributeValuesList from "@/components/admin/attribute_values/AttributeValuesList.vue";

const attributeValues = ref([]);

onMounted(() => reloadAttributeValues());

async function reloadAttributeValues() {
  const resp = await axiosPrivate.get("/attribute-values").catch((error) => console.log(error));

  if (resp.status !== 200) return;

  attributeValues.value = resp.data;
}
</script>

<template>
  <div>
    <attribute-values-form @reload="reloadAttributeValues" />
    <attribute-values-list @reload="reloadAttributeValues" :attributeValues="attributeValues" />
  </div>
</template>
