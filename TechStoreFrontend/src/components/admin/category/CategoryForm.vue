<script setup>
import { ref } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import FormContainer from "@/components/common/FormContainer.vue";
import { ITEM_CREATE_FAIL, ITEM_CREATE_SUCCESS } from "../../../constants/messages/create";

// TODO: Add valdator and init state
// TODO: name and slug can be max 48 characters long
// TODO: add slug regex / format validation (eg. this-is-an-example)
const emit = defineEmits(["reload"]);
const toast = useToast();
const name = ref("");
const slug = ref("");
const loading = ref(false);

async function handleSave() {
  await axiosPrivate
    .post("/categories", {
      name: name.value,
      slug: slug.value,
    })
    .then((resp) => {
      if (resp.status == 200) {
        name.value = "";
        slug.value = "";
        toast.success(ITEM_CREATE_SUCCESS);
        emit("reload");
      }
    })
    .catch((error) => {
      console.log(error);
      toast.error(ITEM_CREATE_FAIL);
    });
}
</script>

<template>
  <form-container title="New Category">
    <v-form @submit.prevent>
      <v-text-field
        v-model="name"
        class="mt-5"
        label="Name"
        density="compact"
        variant="outlined"
        hide-details="auto"
      />
      <v-text-field
        v-model="slug"
        class="mt-5"
        label="Slug"
        density="compact"
        variant="outlined"
        hide-details="auto"
      />
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
