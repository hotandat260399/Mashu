package com.example.ailatrieuphu.Question;


import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.fragment.app.Fragment;

import com.example.ailatrieuphu.R;

/**
 * A simple {@link Fragment} subclass.
 */
public class VanHoa_SuKienQuest extends Fragment {


    public VanHoa_SuKienQuest() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        ViewGroup rootView = (ViewGroup) inflater.inflate(
                R.layout.fragment_van_hoa__su_kien_quest, container, false);
        return rootView;
    }

}
