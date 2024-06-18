<script setup>
import { computed, onMounted, ref, watch } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import FormContainer from "@/components/common/FormContainer.vue";
import { ITEM_CREATE_FAIL, ITEM_CREATE_SUCCESS } from "@/constants/messages/create";
import { ITEM_UPDATE_FAIL, ITEM_UPDATE_SUCCESS } from "@/constants/messages/update";

// TODO: add form validation and state
// TODO: name and slug can be max 48 characters long
// TODO: add slug regex / format validation (eg. this-is-an-example)
const props = defineProps({
  id: {
    type: [String, Number],
    required: false,
    default: null,
  },
});
const emit = defineEmits(["reload", "clearSelectedId"]);
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

watch(
  () => props.id,
  (newId) => {
    if (newId) {
      loadSubcategory(newId);
    } else {
      resetForm();
    }
  },
  { immediate: true }
);

const formTitle = computed(() => (props.id ? "Edit Subcategory" : "New Subcategory"));

async function loadSubcategory(id) {
  try {
    const { data, status } = await axiosPrivate.get(`/subcategories/${id}`);
    if (status === 200) {
      name.value = data.name;
      slug.value = data.slug;
      image.value = data?.imageURL;
      category.value = data?.category;
    }
  } catch (error) {
    console.error(error);
    toast.error("Failed to load subcategory.");
  }
}

function resetForm() {
  props.id = null;
  name.value = "";
  slug.value = "";
  image.value = "";
  category.value = null;
}

async function handleSave() {
  loading.value = true;

  const payload = {
    name: name.value,
    slug: slug.value,
    imageURL: image.value,
    categoryId: category.value,
  };

  try {
    let resp;

    if (props.id) {
      resp = await axiosPrivate.put(`/subcategories/${props.id}`, payload);
    } else {
      resp = await axiosPrivate.post("/subcategories", payload);
    }

    if (resp.status == 200) {
      resetForm();
      toast.success(props?.id ? ITEM_UPDATE_SUCCESS : ITEM_CREATE_SUCCESS);
      emit("clearSelectedId");
      emit("reload");
    }
  } catch (error) {
    console.log(error);
    toast.error(props?.id ? ITEM_UPDATE_FAIL : ITEM_CREATE_FAIL);
  } finally {
    loading.value = false;
  }
}

const handleCancel = () => {
  resetForm();
  emit("clearSelectedId");
};
</script>

<template>
  <form-container :title="formTitle">
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
      <v-btn
        v-if="props?.id"
        type="submit"
        class="form__btn mr-5"
        :loading="loading"
        :disabled="loading"
        @click="handleCancel"
        >Cancel</v-btn
      >
      <v-btn
        type="submit"
        class="form__btn"
        :loading="loading"
        :disabled="loading"
        @click="handleSave"
        >Save</v-btn
      >
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
