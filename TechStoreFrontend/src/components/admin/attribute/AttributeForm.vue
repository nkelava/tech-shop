<script setup>
import { onMounted, ref } from "vue";
import FormContainer from "@/components/common/FormContainer.vue";

const attributeValues = ref([]);
const name = ref("");
const attributeValueIds = ref([]);
const loading = ref(false);

onMounted(() => {
  attributeValues.value = [
    {
      id: 0,
      value: "64 GB",
    },
    {
      id: 1,
      value: "128 GB",
    },
    {
      id: 2,
      value: "3200 Hz",
    },
  ];
});

function handleSave() {
  const formData = new FormData();
  formData.append("name", name.value);
  formData.append("attributeValueIds", attributeValueIds.value);

  console.log(formData);
}
</script>

<template>
  <form-container title="New Attribute">
    <v-form @submit.prevent>
      <v-text-field
        v-model="name"
        class="mt-5"
        label="Name"
        density="compact"
        variant="outlined"
        hide-details="auto"
      />
      <v-select
        v-model="attributeValueIds"
        class="mt-5"
        label="Attribute Values"
        :items="attributeValues"
        item-value="id"
        item-title="value"
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
