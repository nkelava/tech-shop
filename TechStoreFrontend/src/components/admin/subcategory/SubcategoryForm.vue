<script setup>
import { onMounted, ref } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import FormContainer from "@/components/common/FormContainer.vue";
import { ITEM_CREATE_FAIL, ITEM_CREATE_SUCCESS } from "../../../constants/messages/create";

const emit = defineEmits(["reload"]);
const toast = useToast();
const categories = ref([]);
const name = ref("");
const slug = ref("");
const image = ref("");
const category = ref(null);
const loading = ref(false);

onMounted(async () => {
  await axiosPrivate
    .get("/categories")
    .then((resp) => {
      if (resp.status !== 200) return;
      categories.value = resp.data;
    })
    .catch((error) => console.log(error));
});

async function handleSave() {
  // TODO: add form validation and state
  // TODO: name and slug can be max 48 characters long
  // TODO: add slug regex / format validation (eg. this-is-an-example)
  await axiosPrivate
    .post("/subcategories", {
      name: name.value,
      slug: slug.value,
      imageURL: image?.value,
      categoryId: category.value,
    })
    .then((resp) => {
      if (resp.status == 200) {
        name.value = "";
        slug.value = "";
        category.value = null;
        image.value = "";
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
  <form-container title="New Subcategory">
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
      <v-text-field
        v-model="image"
        class="mt-5"
        label="Image URL"
        density="compact"
        variant="outlined"
        hide-details="auto"
      />
      <!-- TODO: Add image upload -->
      <!-- <v-file-input
        v-model="image"
        class="mt-5"
        label="Image"
        density="compact"
        variant="outlined"
        hide-details="auto"
      ></v-file-input> -->
      <v-select
        v-model="category"
        class="mt-5 test"
        label="Category"
        :items="categories"
        item-value="id"
        item-title="name"
        density="compact"
        hide-details="auto"
        variant="outlined"
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

.test * {
  color: coral;
}
</style>
