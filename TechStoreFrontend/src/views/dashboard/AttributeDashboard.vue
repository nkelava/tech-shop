<script setup>
import { onMounted, ref } from "vue";
import { axiosPrivate } from "@/api/axios";
import AttributeForm from "@/components/admin/attribute/AttributeForm.vue";
import AttributeList from "@/components/admin/attribute/AttributeList.vue";

const attributes = ref([]);
const selectedAttributeId = ref(null);

onMounted(() => reloadAttributes());

async function reloadAttributes() {
  const resp = await axiosPrivate.get("/attributes").catch((error) => console.log(error));

  if (resp?.status !== 200) return;

  attributes.value = resp.data;
}

function editAttribute(id) {
  selectedAttributeId.value = id;
}
</script>

<template>
  <div>
    <attribute-form
      :id="selectedAttributeId"
      @reload="reloadAttributes"
      @clearSelectedId="selectedAttributeId = null"
    />
    <attribute-list :attributes="attributes" @reload="reloadAttributes" @edit="editAttribute" />
  </div>
</template>
