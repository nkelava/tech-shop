<script setup>
import { onMounted, ref } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import FormContainer from "@/components/common/FormContainer.vue";
import { ITEM_CREATE_FAIL, ITEM_CREATE_SUCCESS } from "../../../constants/messages/create";

const emit = defineEmits(["reload"]);
const toast = useToast();
const name = ref("Test");
const slug = ref("test");
const summary = ref("This is a summary.");
const description = ref("This is a description.");
const price = ref(1200);
const onSale = ref(false);
const discount = ref(0);
const promoCode = ref(null);
const unitsInStock = ref(25);
const imageURL = ref(null);
const subcategory = ref(null);
const promoCodes = ref([]);
const subcategories = ref([]);
const loading = ref(false);

onMounted(async () => {
  await axiosPrivate
    .get("/subcategories")
    .then((resp) => {
      if (resp?.status !== 200) return;
      subcategories.value = resp.data;
    })
    .catch((error) => console.log(error));

  await axiosPrivate
    .get("/promo-codes")
    .then((resp) => {
      if (resp?.status !== 200) return;
      promoCodes.value = resp.data;
    })
    .catch((error) => console.log(error));
});

async function handleSave() {
  await axiosPrivate
    .post("/products", {
      name: name.value,
      slug: slug.value,
      imageURL: imageURL?.value,
      summary: summary.value,
      description: description.value,
      onSale: onSale.value,
      price: price.value,
      unitsInStock: unitsInStock.value,
      promoCodeId: promoCode.value,
      subcategoryId: subcategory.value,
    })
    .then((resp) => {
      if (resp.status == 200) {
        name.value = "Test";
        slug.value = "test";
        summary.value = "This is a summary.";
        description.value = "This is a description.";
        price.value = 1200;
        onSale.value = false;
        discount.value = 0;
        promoCode.value = null;
        unitsInStock.value = 25;
        imageURL.value = null;
        subcategory.value = null;
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
  <form-container title="New Product">
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
        v-model="summary"
        class="mt-5"
        label="Summary"
        density="compact"
        variant="outlined"
        hide-details="auto"
      />
      <v-textarea
        v-model="description"
        class="mt-5"
        label="Description"
        variant="outlined"
        hide-details="auto"
      />
      <v-text-field
        v-model="price"
        type="number"
        class="mt-5"
        label="Price"
        density="compact"
        variant="outlined"
        hide-details="auto"
        min="0"
      />
      <v-checkbox v-model="onSale" class="mt-5" label="On Sale" hide-details="auto"></v-checkbox>
      <v-text-field
        v-model="discount"
        type="number"
        class="mt-5"
        label="Discount"
        density="compact"
        variant="outlined"
        hide-details="auto"
        min="0"
        max="100"
        :disabled="!onSale"
      />
      <v-select
        v-model="promoCode"
        class="mt-5"
        label="Promo Code"
        :items="promoCodes"
        item-value="id"
        item-title="code"
        density="compact"
        hide-details="auto"
        variant="outlined"
      ></v-select>
      <v-text-field
        v-model="unitsInStock"
        type="number"
        class="mt-5"
        label="Units in stock"
        density="compact"
        variant="outlined"
        hide-details="auto"
        min="0"
      />
      <v-file-input
        v-model="imageURL"
        class="mt-5"
        label="Image"
        density="compact"
        variant="outlined"
        hide-details="auto"
      ></v-file-input>
      <v-select
        v-model="subcategory"
        class="mt-5"
        label="Subcategory"
        :items="subcategories"
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

.attribute__add {
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 10px;
}

.attribute__list {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  min-height: 50px;
  margin-top: 15px;
  border: 2px dashed var(--ts-c-bg-light);
  border-radius: 5px;
}

.btn--add {
  margin-top: 20px;
  font-weight: bold;
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
}

.chip {
  display: flex;
  margin: 5px;
}

.chip__btn {
  margin-left: 10px;
  background: transparent;
  color: var(--ts-c-danger);
  border: none;
  outline: none;
  box-shadow: none !important;
}

@media only screen and (min-width: 40em) {
  .attribute__add {
    flex-direction: row;
    align-items: center;
  }
}
</style>
