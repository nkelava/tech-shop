<script setup>
import { computed, onMounted, ref, watch } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import FormContainer from "@/components/common/FormContainer.vue";
import { ITEM_CREATE_FAIL, ITEM_CREATE_SUCCESS } from "@/constants/messages/create";
import { ITEM_UPDATE_FAIL, ITEM_UPDATE_SUCCESS } from "@/constants/messages/update";

// TODO: Add valdator and init state
const props = defineProps({
  id: {
    type: [String, Number],
    required: false,
    default: null,
  },
});
const emit = defineEmits(["reload", "clearSelectedId"]);
const toast = useToast();
const name = ref("Acer Predator Helios 300");
const slug = ref("acer-predator-helios-300");
const summary = ref(
  'Acer Predator Helios 300, 15.6" Full HD IPS, Intel i7 CPU, 16GB DDR4 RAM, 256GB SSD, GeForce GTX 1060, VR Ready, Red Backlit KB, Metal Chassis, Windows 10 64-bit, G3-571-77QK'
);
const description = ref(
  'Latest 7th Generation Intel Core i7 Processor 2.8GHz with Turbo Boost Technology up to 3.8GHz | Windows 10 Home 64-bit Latest NVIDIA GeForce GTX 1060 with 6 GB of dedicated GDDR5 VRAM 15.6" Full HD (1920 x 1080) widescreen IPS display, Red Backlit Keyboard 16GB DDR4 DRAM Memory & 256GB SSD | Extra empty expandable hard drive slot for 2.5" hard drives. Up to 7 - hours of battery life.'
);
const price = ref(1300);
const onSale = ref(false);
const discount = ref(0);
const promoCode = ref(null);
const unitsInStock = ref(25);
const imageURL = ref("https://www.mikronis.hr/_shop/files/products/Helios300-bk.jpg?id=248");
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
      name.value = data.name;
      slug.value = data.slug;
      summary.value = data.summary;
      description.value = data.description;
      price.value = data.price;
      onSale.value = data.onSale;
      discount.value = data.discount;
      promoCode.value = data.promoCode;
      unitsInStock.value = data.unitsInStock;
      imageURL.value = data.imageURL;
      subcategory.value = data.subcategory;
    }
  } catch (error) {
    console.error(error);
    toast.error("Failed to load product.");
  }
}

function resetForm() {
  props.id = null;
  name.value = "Acer Predator Helios 300";
  slug.value = "acer-predator-helios-300";
  summary.value =
    'Acer Predator Helios 300, 15.6" Full HD IPS, Intel i7 CPU, 16GB DDR4 RAM, 256GB SSD, GeForce GTX 1060, VR Ready, Red Backlit KB, Metal Chassis, Windows 10 64-bit, G3-571-77QK';
  description.value =
    'Latest 7th Generation Intel Core i7 Processor 2.8GHz with Turbo Boost Technology up to 3.8GHz | Windows 10 Home 64-bit Latest NVIDIA GeForce GTX 1060 with 6 GB of dedicated GDDR5 VRAM 15.6" Full HD (1920 x 1080) widescreen IPS display, Red Backlit Keyboard 16GB DDR4 DRAM Memory & 256GB SSD | Extra empty expandable hard drive slot for 2.5" hard drives. Up to 7 - hours of battery life.';
  price.value = 1200;
  onSale.value = false;
  discount.value = 0;
  promoCode.value = null;
  unitsInStock.value = 25;
  imageURL.value = "https://www.mikronis.hr/_shop/files/products/Helios300-bk.jpg?id=248";
  subcategory.value = null;
}

const handleSave = async () => {
  loading.value = true;

  const payload = {
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
    console.log(error);
    toast.error(props?.id ? ITEM_UPDATE_FAIL : ITEM_CREATE_FAIL);
  } finally {
    loading.value = false;
  }
};

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
      <v-text-field
        v-model="imageURL"
        class="mt-5"
        label="Image URL"
        density="compact"
        variant="outlined"
        hide-details="auto"
      />
      <!-- TODO: Add image upload -->
      <!-- <v-file-input
        v-model="imageURL"
        class="mt-5"
        label="Image"
        density="compact"
        variant="outlined"
        hide-details="auto"
      ></v-file-input> -->
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
</style>
