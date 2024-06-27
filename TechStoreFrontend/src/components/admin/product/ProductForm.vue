<script setup>
import { computed, onMounted, reactive, ref, watch } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import BaseInput from "@/components/common/BaseInput.vue";
import FormContainer from "@/components/common/FormContainer.vue";
import { initialProductState, productRules } from "@/vuelidate/product";
import { ITEM_CREATE_FAIL, ITEM_CREATE_SUCCESS } from "@/constants/messages/create";
import { ITEM_UPDATE_FAIL, ITEM_UPDATE_SUCCESS } from "@/constants/messages/update";

const props = defineProps({
  id: {
    type: [String, Number],
    required: false,
    default: null,
  },
});
const emit = defineEmits(["reload", "clearSelectedId"]);
const toast = useToast();
const productState = reactive({ ...initialProductState });
const promoCodes = ref([]);
const subcategories = ref([]);
const loading = ref(false);

const v$ = useVuelidate(productRules, productState);

onMounted(async () => {
  await axiosPrivate
    .get("/subcategories")
    .then((resp) => {
      if (resp?.status !== 200) return;
      subcategories.value = resp.data;
    })
    .catch((error) => {
      toast.error("Failed to load subcategories.");
      console.log(error);
    });

  await axiosPrivate
    .get("/promo-codes")
    .then((resp) => {
      if (resp?.status !== 200) return;
      promoCodes.value = resp.data;
    })
    .catch((error) => {
      toast.error("Failed to load promo codes.");
      console.log(error);
    });
});

watch(
  () => props?.id,
  (newId) => {
    if (newId) {
      loadProduct(newId);
    } else {
      resetForm();
    }
  },
  { immediate: true }
);

const formTitle = computed(() => (props.id ? "Edit Product" : "New Product"));

async function loadProduct(id) {
  try {
    const { data, status } = await axiosPrivate.get(`/products/${id}`);

    if (status === 200) {
      productState.name = data?.name;
      productState.slug = data?.slug;
      productState.summary = data?.summary;
      productState.description = data?.description;
      productState.price = data?.price;
      productState.onSale = data?.onSale;
      productState.discount = data?.discount;
      productState.promoCode = data?.promoCode;
      productState.unitsInStock = data?.unitsInStock;
      productState.imageURL = data?.imageURL;
      productState.subcategory = data?.subcategory?.id;
    }

    console.log("product: ", productState);
  } catch (error) {
    toast.error("Failed to load product.");
    console.error(error);
  }
}

const handleSave = async () => {
  if (!(await v$.value.$validate())) return;

  loading.value = true;

  const payload = {
    name: productState?.name,
    slug: productState?.slug,
    imageURL: productState?.imageURL,
    summary: productState?.summary,
    description: productState?.description,
    onSale: productState?.onSale,
    price: productState?.price,
    unitsInStock: productState?.unitsInStock,
    promoCodeId: productState?.promoCode,
    subcategoryId: productState?.subcategory,
  };

  try {
    let resp;

    if (props?.id) {
      resp = await axiosPrivate.put(`/products/${props.id}`, payload);
    } else {
      resp = await axiosPrivate.post("/products", payload);
    }

    if (resp.status == 200) {
      resetForm();
      toast.success(props?.id ? ITEM_UPDATE_SUCCESS : ITEM_CREATE_SUCCESS);
      emit("clearSelectedId");
      emit("reload");
    }
  } catch (error) {
    toast.error(
      error?.response?.data ? error.response.data : props?.id ? ITEM_UPDATE_FAIL : ITEM_CREATE_FAIL
    );
    console.log(error);
  } finally {
    loading.value = false;
  }
};

const handleCancel = () => {
  resetForm();
  emit("clearSelectedId");
};

function resetForm() {
  props.id = null;
  v$.value.$reset();
  Object.assign(productState, initialProductState);
}
</script>

<template>
  <form-container :title="formTitle">
    <v-form @submit.prevent="handleSave">
      <base-input
        v-model="productState.name"
        class="mt-5"
        name="name"
        label="Name"
        :v$="v$.name"
        density="compact"
        hide-details="auto"
      />
      <base-input
        v-model="productState.slug"
        class="mt-5"
        name="slug"
        label="Slug"
        :v$="v$.slug"
        density="compact"
        hide-details="auto"
      />
      <base-input
        v-model="productState.summary"
        class="mt-5"
        name="summary"
        label="Summary"
        :v$="v$.summary"
        density="compact"
        hide-details="auto"
      />
      <v-textarea
        v-model="productState.description"
        class="mt-5"
        name="description"
        label="Description"
        variant="outlined"
        hide-details="auto"
        :error-messages="v$?.description?.$errors.map((e) => e.$message)"
      />
      <base-input
        v-model="productState.price"
        type="number"
        class="mt-5"
        name="price"
        label="Price"
        :v$="v$.price"
        density="compact"
        hide-details="auto"
        min="0"
      />
      <v-checkbox
        v-model="productState.onSale"
        class="mt-5"
        label="On Sale"
        hide-details="auto"
      ></v-checkbox>
      <base-input
        v-model="productState.discount"
        type="number"
        class="mt-5"
        label="Discount"
        density="compact"
        variant="outlined"
        hide-details="auto"
        min="0"
        max="100"
        :disabled="!productState.onSale"
      />
      <!-- <v-select
        v-model="productState.promoCode"
        class="mt-5"
        label="Promo Code"
        :items="promoCodes"
        item-value="id"
        item-title="code"
        density="compact"
        hide-details="auto"
        variant="outlined"
        clearable
      ></v-select> -->
      <base-input
        v-model="productState.unitsInStock"
        type="number"
        class="mt-5"
        label="Units in stock"
        density="compact"
        variant="outlined"
        hide-details="auto"
        min="0"
      />
      <!-- TODO: Add image upload -->
      <base-input
        v-model="productState.imageURL"
        class="mt-5"
        name="imageURL"
        label="Image URL"
        :v$="v$.imageURL"
        density="compact"
        hide-details="auto"
      />
      <v-select
        v-model="productState.subcategory"
        class="mt-5"
        name="subcategory"
        label="Subcategory"
        :items="subcategories"
        item-value="id"
        item-title="name"
        density="compact"
        hide-details="auto"
        variant="outlined"
        :error-messages="v$?.subcategory?.$errors.map((e) => e.$message)"
      ></v-select>
      <v-btn
        v-if="props?.id"
        type="button"
        class="form__btn mr-5"
        :loading="loading"
        :disabled="loading"
        @click="handleCancel"
        >Cancel</v-btn
      >
      <v-btn type="submit" class="form__btn" :loading="loading" :disabled="loading">Save</v-btn>
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
