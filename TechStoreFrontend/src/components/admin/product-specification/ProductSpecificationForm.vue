<script setup>
import { onMounted, ref } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import FormContainer from "@/components/common/FormContainer.vue";
import { ITEM_CREATE_FAIL, ITEM_CREATE_SUCCESS } from "../../../constants/messages/create";

const emit = defineEmits(["reload"]);
const toast = useToast();
const product = ref(null);
const attribute = ref(null);
const attributeValue = ref(null);
const productAttributes = ref([]);
const products = ref([]);
const attributes = ref([]);
const attributeValues = ref([]);
const loading = ref(false);

onMounted(async () => {
  await axiosPrivate
    .get("/products")
    .then((resp) => {
      if (resp?.status !== 200) return;
      products.value = resp.data;
    })
    .catch((error) => console.log(error));

  await axiosPrivate
    .get("/attributes")
    .then((resp) => {
      if (resp?.status !== 200) return;
      attributes.value = resp.data;
    })
    .catch((error) => console.log(error));

  await axiosPrivate
    .get("/attribute-values")
    .then((resp) => {
      if (resp?.status !== 200) return;
      attributeValues.value = resp.data;
    })
    .catch((error) => console.log(error));
});

async function handleSave() {
  await axiosPrivate
    .post("/products/specification", {
      productId: product.value,
      productAttributes: productAttributes.value,
    })
    .then((resp) => {
      product.value = null;
      attribute.value = null;
      attributeValue.value = null;
      productAttributes.value = null;
      toast.success(ITEM_CREATE_SUCCESS);
      emit("relaod");
    })
    .catch((error) => {
      console.log(error);
      toast.error(ITEM_CREATE_FAIL);
    });
}

function removeAttribute(attributeId, attributeValueId) {
  productAttributes.value = productAttributes.value.filter(
    (pa) => pa.attribute.id !== attributeId || pa.attributeValue.id !== attributeValueId
  );
}

function handleAttributeValuePairAdd() {
  if (attribute.value !== null && attributeValue.value !== null) {
    const pairExists = productAttributes.value.find(
      (av) => av.attribute.id === attribute.value && av.attributeValue.id === attributeValue.value
    );

    if (!pairExists) {
      productAttributes.value.push({
        attributeId: attribute.value,
        attribute: attributes.value.find((a) => a.id === attribute.value),
        attributeValueId: attributeValue.value,
        attributeValue: attributeValues.value.find((av) => av.id === attributeValue.value),
      });

      attribute.value = null;
      attributeValue.value = null;
    }
  }
}
</script>

<template>
  <form-container title="Product Specification">
    <v-form @submit.prevent>
      <v-select
        v-model="product"
        class="mt-5"
        label="Product"
        :items="products"
        item-value="id"
        :item-title="(item) => `${item.name} (${item.id})`"
        density="compact"
        hide-details="auto"
        variant="outlined"
      />

      <div class="attribue-container">
        <div class="attribute__add">
          <v-select
            v-model="attribute"
            class="mt-5"
            label="Attribute"
            :items="attributes"
            item-value="id"
            item-title="name"
            density="compact"
            hide-details="auto"
            variant="outlined"
          />
          <v-select
            v-model="attributeValue"
            label="Attribute Value"
            class="mt-sm-5"
            :items="attributeValues"
            item-value="id"
            item-title="value"
            density="compact"
            hide-details="auto"
            variant="outlined"
          />
          <v-btn class="btn--add" @click="handleAttributeValuePairAdd">Add</v-btn>
        </div>
        <div class="attribute__list">
          <v-chip v-for="(attributeValuePair, i) in productAttributes" :key="i" class="chip" label>
            {{ attributeValuePair.attribute.name }} {{ attributeValuePair.attributeValue.value }}
            <v-btn
              class="chip__btn"
              icon="mdi-close-circle-outline"
              density="compact"
              @click="
                removeAttribute(
                  attributeValuePair.attribute.id,
                  attributeValuePair.attributeValue.id
                )
              "
            ></v-btn>
          </v-chip>
        </div>
      </div>
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
