<script setup>
import { onMounted, ref } from "vue";
import { axiosPrivate } from "@/api/axios";
import AttributeValuesForm from "@/components/admin/attribute_values/AttributeValuesForm.vue";
import AttributeValuesList from "@/components/admin/attribute_values/AttributeValuesList.vue";

const attributeValues = ref([]);
const selectedAttributeValueId = ref(null);

onMounted(() => reloadAttributeValues());

async function reloadAttributeValues() {
  const resp = await axiosPrivate.get("/attribute-values").catch((error) => console.log(error));

  if (resp?.status !== 200) return;

  attributeValues.value = resp.data;
}

function editAttributeValue(id) {
  selectedAttributeValueId.value = id;
}
</script>

<template>
  <div>
    <attribute-values-form
      :id="selectedAttributeValueId"
      @reload="reloadAttributeValues"
      @clearSelectedId="selectedAttributeValueId = null"
    />
    <attribute-values-list
      :attributeValues="attributeValues"
      @reload="reloadAttributeValues"
      @edit="editAttributeValue"
    />
  </div>
</template>
