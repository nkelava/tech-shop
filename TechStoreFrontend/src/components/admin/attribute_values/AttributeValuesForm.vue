<script setup>
import { onMounted, ref } from "vue";
import FormContainer from "@/components/common/FormContainer.vue";

const attributes = ref([]);
const value = ref("");
const attributeIds = ref([]);
const loading = ref(false);

onMounted(() => {
  attributes.value = [
    {
      id: 0,
      name: "RAM",
    },
    {
      id: 1,
      name: "SSD",
    },
    {
      id: 2,
      name: "HDD",
    },
  ];
});

function handleSave() {
  const formData = new FormData();
  formData.append("value", value.value);
  formData.append("attributeIds", attributeIds.value);

  console.log(formData);
}
</script>

<template>
  <form-container title="New Attribute Value">
    <v-form @submit.prevent>
      <v-text-field
        v-model="value"
        class="mt-5"
        label="Value"
        density="compact"
        variant="outlined"
        hide-details="auto"
      />
      <v-select
        v-model="attributeIds"
        class="mt-5"
        label="Attributes"
        :items="attributes"
        item-value="id"
        item-title="name"
        density="compact"
        hide-details="auto"
        variant="outlined"
        multiple
      ></v-select>
      <v-btn type="submit" class="form__btn" :loading="loading" @click="handleSave">Save</v-btn>
    </v-form>
  </form-container>
</template>

<style scoped>
.form__btn {
  margin-top: 30px;
  font-weight: bold;
  background: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
  width: 100%;
  max-width: 300px !important;
}
</style>
